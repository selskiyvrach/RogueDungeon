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
        private float _currentHealth;

        public Transform CameraPovPoint { get; }
        public PlayerBlockerHandler BlockerHandler { get; }
        public PlayerDodgeState DodgeState
        {
            get => _dodger.DodgeState;
            set => _dodger.DodgeState = value;
        }
        public bool IsAlive => _currentHealth > 0;

        public Player(PlayerConfig config, PlayerGameObject gameObject, Level level, PlayerPositionInTheMaze playerMazePosition, IDodger dodger, PlayerBlockerHandler blockerHandler)
        {
            _config = config;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            _dodger = dodger;
            BlockerHandler = blockerHandler;
            CameraPovPoint = gameObject.CameraReferencePoint;
        }

        public void SetBehaviours(PlayerHandsBehaviour handsBehaviour, PlayerMovementBehaviour movementBehaviour)
        {
            _playerHandsBehaviour = handsBehaviour;
            _movementBehaviour = movementBehaviour;   
        }

        public void Initialize()
        {
            _level.LevelTraverser = _mazeTraversalPointer;
            _movementBehaviour.Initialize();
            _playerHandsBehaviour.Initialize();
            ((IHandheldContext)_playerHandsBehaviour).IntendedItem = new Item(_config.DefaultWeapon);
            _currentHealth = _config.Health;
        }

        public void TakeHitDamage(float damage) => 
            _currentHealth -= damage;

        public void Tick(float deltaTime)
        {
            if(!IsAlive)
                return;
            _movementBehaviour.Tick(deltaTime);
            _playerHandsBehaviour.Tick(deltaTime);
        }
    }
}