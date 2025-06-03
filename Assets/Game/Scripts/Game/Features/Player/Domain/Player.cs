using System;
using Game.Features.Player.Domain.Behaviours.Hands;
using Game.Libs.InGameResources;
using Game.Libs.Input;
using Libs.Fsm;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Features.Player.Domain
{
    public class Player : IInitializable, ITickable, IDisposable
    {
        private StateMachine _movement;

        public PlayerConfig Config { get; }
        public PlayerHandsBehaviour Hands { get; private set; }
        public Resource Health { get; }
        public RechargeableResource Stamina { get; }
        public DodgeState DodgeState { get; set; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsHoldingBreath { get; set; }
        public bool IsAlive => Health.Current > 0;
        public float DoubleGripDamageBonus => Config.DoubleGripDamageBonus;
        public float DoubleGripBlockBonus => Config.DoubleGripBlockBonus;
        public bool HasUnabsorbedBlockImpact { get; set; }
        public IBlockingItem BlockingItem { get; set; }
        public float DodgeStaminaCost => Config.DodgeStaminaCost;

        public Player(PlayerConfig config)
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
            _movement = movementBehaviour; 
        }

        public void Initialize()
        {
            _movement.Initialize();
            Hands.Initialize();
            Hands.Enable();
        }

        public void Tick(float deltaTime)
        {
            _movement.Tick(deltaTime);

            if (!IsAlive)
                return;

            Hands.Tick(deltaTime);
            Stamina.Tick(deltaTime);
        }

        public void ShowInventory()
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}