using Common.Lifecycle;
using RogueDungeon.Characters;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Levels;
using RogueDungeon.Player.Model.Behaviours.Common;
using RogueDungeon.Player.Model.Behaviours.Hands;
using UnityEngine;

namespace RogueDungeon.Player.Model
{
    public class Player : IInitializable, ITickable
    {
        private readonly PlayerConfig _config;
        private readonly PlayerPositionInTheMaze _mazeTraversalPointer;
        private readonly Level _level;
        private readonly IPlayerInput _input;
        
        private PlayerCommonBehaviour _commonBehaviour;

        public PlayerHandsBehaviour Hands { get; private set; }
        public Transform CameraPovPoint { get; }
        public Resource Health { get; }
        public RechargeableResource Stamina { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsAlive => Health.Current > 0;
        public float DoubleGripDamageBonus => _config.DoubleGripDamageBonus;
        public float DoubleGripBlockBonus => _config.DoubleGripBlockBonus;
        public bool HasUnabsorbedBlockImpact { get; set; }
        public IItem BlockingItem { get; set; }
        public bool IsBlocking { get; set; }

        public Player(PlayerConfig config, PlayerGameObject gameObject, Level level, PlayerPositionInTheMaze playerMazePosition, IPlayerInput input)
        {
            _config = config;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            _input = input;
            CameraPovPoint = gameObject.CameraReferencePoint;
            Stamina = new RechargeableResource(_config.Stamina);
            Health = new Resource(_config.Health);
            Stamina.Refill();
            Health.Refill();
        }

        public void SetBehaviours(PlayerHandsBehaviour handsBehaviour, PlayerCommonBehaviour commonBehaviour)
        {
            Hands = handsBehaviour;
            _commonBehaviour = commonBehaviour;   
        }

        public void Initialize()
        {
            _level.LevelTraverser = _mazeTraversalPointer;
            _commonBehaviour.Initialize();
            Hands.Initialize();
        }

        public void Tick(float deltaTime)
        {
            if (IsAlive)
            {
                if (_input.HasInput(InputKey.CycleLeftArmItems))
                {
                    Hands.LeftHand.IntendedItem = Hands.LeftHand.IntendedItem != null ? null : new Shield(_config.DefaultShield);
                    _input.ConsumeInput(InputKey.CycleLeftArmItems);
                }
                if (_input.HasInput(InputKey.CycleRightArmItems))
                {
                    Hands.RightHand.IntendedItem = Hands.RightHand.IntendedItem != null ? null : new Weapon(_config.DefaultWeapon);
                    _input.ConsumeInput(InputKey.CycleRightArmItems);
                }
            }

            if(IsAlive)
                Stamina.Tick(deltaTime);
            _commonBehaviour.Tick(deltaTime);
            if(IsAlive)
                Hands.Tick(deltaTime);
        }
    }
}