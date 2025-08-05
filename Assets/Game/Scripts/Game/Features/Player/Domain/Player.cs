using System;
using Game.Features.Player.Domain.Behaviours.Hands;
using Game.Libs.Combat;
using Game.Libs.InGameResources;
using Game.Libs.Items;
using Libs.Fsm;
using Libs.Lifecycle;

namespace Game.Features.Player.Domain
{
    public class Player : IInitializable, ITickable, IDisposable, IPlayerDefenderInfoProvider
    {
        private StateMachine _movementStateMachine;
        public IPlayerConfig Config { get; }
        public PlayerHandsBehaviour Hands { get; private set; }
        public Resource Health { get; }
        public RechargeableResource Stamina { get; }
        public DodgeState DodgeState { get; set; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsHoldingBreath { get; set; }
        public bool IsAlive => Health.Current > 0;
        public float DodgeStaminaCost => Config.DodgeStaminaCost;
        public bool IsInCombat { get; set; }
        public IBlockingItem BlockingItem { get; set; }
        public bool HasUnabsorbedBlockImpact { get; set; }

        public event Action<PlayerAttackInfo> OnAttackMediationRequested;
        public event Action OnShowInventoryRequested;

        public Player(IPlayerConfig config)
        {
            Config = config;
            Stamina = new RechargeableResource(Config.Stamina);
            Health = new Resource(Config.Health);
            Stamina.Refill();
            Health.Refill();
        }

        public void SetBehaviours(PlayerHandsBehaviour handsBehaviour, StateMachine movementBehaviour)
        {
            Hands = handsBehaviour;
            _movementStateMachine = movementBehaviour; 
        }

        public void Initialize()
        {
            _movementStateMachine.Initialize();
            Hands.Initialize();
        }

        public void Tick(float deltaTime)
        {
            _movementStateMachine.Tick(deltaTime);

            if (!IsAlive)
                return;

            Hands.Tick(deltaTime);
            Stamina.Tick(deltaTime);
        }

        public DefenderInfo GetDefenderInfo() =>
            new(
                isAlive: IsAlive,
                dodgingAgainst: DodgeState switch
                {
                    DodgeState.DodgingLeft => AttackDirection.Right,
                    DodgeState.DodgingRight => AttackDirection.Left,
                    _ => AttackDirection.None
                },
                isBlocking: BlockingItem != null,
                blockingAbsorbtion: 1,
                blockingStaminaCostFactor: BlockingItem?.BlockStaminaCostMultiplier ?? 1);

        public void ShowInventory() => 
            OnShowInventoryRequested?.Invoke();

        public void Dispose()
        {
            
        }

        public void PerformAttack(IWeapon weapon) => 
            OnAttackMediationRequested?.Invoke(new PlayerAttackInfo(weapon.Damage, weapon.PoiseDamage, EnemyPosition.Middle));
        
        public void SetPlayerAttackResult(PlayerAttackResult result)
        {
            
        }

        public void SetEnemyAttackResult(EnemyAttackResult result)
        {
            if (result.IsDodge)
            {
                // on dodged
                return;
            }

            if (result.IsBlock) 
                HasUnabsorbedBlockImpact = true;
                
            if(!result.IsHit)
                return;
            
            Health.AddDelta(- result.FinalDamage);
            Stamina.AddDelta(- result.FinalStaminaDamage);
        }
    }
}