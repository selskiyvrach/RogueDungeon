using Characters;
using Common.Lifecycle;
using RogueDungeon.Items;
using RogueDungeon.Levels;
using RogueDungeon.Player.Behaviours.Hands;
using RogueDungeon.Player.Behaviours.Movement;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class Player : IDodger, IInitializable, ITickable
    {
        private readonly PlayerConfig _config;
        private readonly PlayerPositionInTheMaze _mazeTraversalPointer;
        private readonly IDodger _dodger;
        private readonly Level _level;
        
        private PlayerMovementBehaviour _movementBehaviour;
        private PlayerHandsBehaviour _playerHandsBehaviour;

        public Transform CameraPovPoint { get; }
        public PlayerBlockerHandler BlockerHandler { get; }
        public Health Health { get; } = new();
        public Stamina.Stamina Stamina { get; }
        public PlayerDodgeState DodgeState
        {
            get => _dodger.DodgeState;
            set => _dodger.DodgeState = value;
        }

        public Player(PlayerConfig config, PlayerGameObject gameObject, Level level, PlayerPositionInTheMaze playerMazePosition, IDodger dodger)
        {
            _config = config;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            _dodger = dodger;
            BlockerHandler = new PlayerBlockerHandler(this);
            CameraPovPoint = gameObject.CameraReferencePoint;
            Stamina = new Stamina.Stamina(_config.StaminaConfig);
        }

        public void SetBehaviours(PlayerHandsBehaviour handsBehaviour, PlayerMovementBehaviour movementBehaviour)
        {
            _playerHandsBehaviour = handsBehaviour;
            _movementBehaviour = movementBehaviour;   
        }

        public void Initialize()
        {
            Stamina.Refill();
            Health.Max = _config.Health;
            Health.Current = _config.Health;
            _level.LevelTraverser = _mazeTraversalPointer;
            _movementBehaviour.Initialize();
            _playerHandsBehaviour.Initialize();
            ((IHandheldContext)_playerHandsBehaviour).IntendedItem = new Item(_config.DefaultWeapon);
        }

        public void TakeHitDamage(float damage) => 
            Health.Current -= damage;

        public void Tick(float deltaTime)
        {
            if(!Health.IsAlive)
                return;
            Stamina.Tick(deltaTime);
            _movementBehaviour.Tick(deltaTime);
            _playerHandsBehaviour.Tick(deltaTime);
        }
    }
}