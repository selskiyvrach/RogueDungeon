using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Combat;
using Game.Libs.InGameResources;
using Game.Libs.WorldObjects;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Features.Combat.Domain.Enemies
{
    public class Enemy : IInitializable, ITickable
    {
        private readonly IEnemyStatesProvider _statesProvider;
        private readonly EnemyStateMachine _stateMachine;
        private readonly EnemyImpactAnimator _impactAnimator;
        private bool _isStunnedBackingField;

        public EnemyConfig Config { get; }
        public EnemyPosition TargetablePosition { get; set; }
        public EnemyPosition OccupiedPosition { get; set; }
        public EnemyCombo CurrentCombo { get; }
        public Resource Health { get; }
        public RechargeableResource Poise { get; }
        public ITwoDWorldObject WorldObject { get; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsIdle { get; set; }
        public bool IsMoving { get; set; }
        public bool IsStunned
        {
            get => _isStunnedBackingField;
            set
            {
                _isStunnedBackingField = value;
                OnStunnedStatusChanged?.Invoke();
            }
        }

        public event Action OnStunnedStatusChanged;
        public bool IsStunnedOrDead => !IsAlive || IsStunned;
        public EnemyAttackMoveConfig[] Attacks => Config.Attacks;
        public bool IsAlive => Health.Current > 0;

        public Enemy(EnemyConfig config, GameObject gameObject, EnemyStateMachine stateMachine, IEnemyStatesProvider statesProvider, EnemyImpactAnimator impactAnimator)
        {  
            WorldObject = new TwoDWorldObject(gameObject);
            Config = config;
            _stateMachine = stateMachine;
            _statesProvider = statesProvider;
            _impactAnimator = impactAnimator;
            Health = new Resource(config.Health);
            Poise = new RechargeableResource(config.Poise);
            CurrentCombo = new EnemyCombo(this);
            Health.Refill();
            Poise.Refill();
        }

        public void Tick(float deltaTime)
        {
            Poise.Tick(deltaTime);
            CurrentCombo.Tick(deltaTime);
            _stateMachine.Tick(deltaTime);
            _impactAnimator.Tick(deltaTime);
        }

        public void Initialize()
        {
            TargetablePosition = OccupiedPosition;
            _stateMachine.Initialize(_statesProvider.GetState(MoveNames.IDLE));
            _stateMachine.TryStartState(_statesProvider.GetState(MoveNames.BIRTH));
        }

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public void ChangePosition(EnemyPosition position)
        {
            var state = (EnemyMovementMove)_statesProvider.GetState(MoveNames.MOVE);
            state.TargetPosition = position;
            _stateMachine.TryStartState(state);
        }

        public bool HasAttacksForCurrentPosition(out IEnumerable<EnemyAttackMoveConfig> moves) => 
            (moves = Attacks.Where(n => (n.SuitableForPositions & OccupiedPosition) != 0)).Any();

        public void StartMove(string name) => 
            _stateMachine.TryStartState(_statesProvider.GetState(name));

        public EnemyAttackInfo GetAttackInfo(EnemyAttackMoveConfig config) => 
            new(config.Damage, config.Direction);

        public void SetPlayerAttackResult(PlayerAttackResult result) => 
            TakeDamage(result.FinalDamage, result.FinalPoiseDamage);

        private void TakeDamage(float damage, float poiseDamage)
        {
            if (IsStunned)
            {
                _stateMachine.StartState(_statesProvider.GetState(MoveNames.IDLE));
                damage *= 2;
            }
            
            Health.AddDelta(-damage);
            Poise.AddDelta(-poiseDamage);
            if (!IsAlive) 
                _stateMachine.TryStartState(_statesProvider.GetState(MoveNames.DEATH));

            if (Poise.Current == 0) 
                _stateMachine.TryStartState(_statesProvider.GetState(MoveNames.STAGGER));

            if(damage > 0 || poiseDamage > 0)
                _impactAnimator.OnHit();
        }

        public void SetOwnAttackResult(EnemyAttackResult result)
        {
        }
    }
}