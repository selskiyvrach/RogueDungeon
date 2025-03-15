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
        private readonly PlayerMovementBehaviour _movementBehaviour;
        private readonly PlayerHandsBehaviour _playerHandsBehaviour;
        private readonly PlayerPositionInTheMaze _mazeTraversalPointer;
        private readonly Level _level;
        
        private float _currentHealth;
        
        public Transform CameraPovPoint { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsAlive => _currentHealth > 0;

        public Player(PlayerConfig config, PlayerHandsBehaviour playerHandsBehaviour, PlayerGameObject gameObject,
            PlayerMovementBehaviour movementBehaviour, Level level, PlayerPositionInTheMaze playerMazePosition)
        {
            _config = config;
            _playerHandsBehaviour = playerHandsBehaviour;
            _movementBehaviour = movementBehaviour;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            CameraPovPoint = gameObject.CameraReferencePoint;
        }

        public void Initialize()
        {
            _level.LevelTraverser = _mazeTraversalPointer;
            _movementBehaviour.Initialize();
            _playerHandsBehaviour.Initialize();
            ((IHandheldContext)_playerHandsBehaviour).IntendedItem = new Item(_config.DefaultWeapon);
            _currentHealth = _config.Health;
        }

        public void TakeDamage(float damage) => 
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