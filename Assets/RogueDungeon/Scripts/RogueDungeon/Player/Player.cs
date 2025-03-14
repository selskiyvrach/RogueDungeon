using Common.Animations;
using Common.Lifecycle;
using RogueDungeon.Items;
using RogueDungeon.Levels;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class Player : IDodger, IInitializable, ITickable
    {
        private readonly PlayerConfig _config;
        private readonly IPlayerMovementBehaviour _levelTraverserBehaviour;
        private readonly PlayerHandsBehaviour _playerHandsBehaviour;
        private readonly PlayerPositionInTheMaze _mazeTraversalPointer;
        private readonly Level _level;
        
        private float _currentHealth;
        
        public Transform CameraPovPoint { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsAlive => _currentHealth > 0;

        public Player(PlayerConfig config, PlayerHandsBehaviour playerHandsBehaviour, PlayerGameObject gameObject,
            IPlayerMovementBehaviour levelTraverserBehaviour, Level level, PlayerPositionInTheMaze playerMazePosition)
        {
            _config = config;
            _playerHandsBehaviour = playerHandsBehaviour;
            _levelTraverserBehaviour = levelTraverserBehaviour;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            CameraPovPoint = gameObject.CameraReferencePoint;
        }

        public void Initialize()
        {
            _level.LevelTraverser = _mazeTraversalPointer;
            _levelTraverserBehaviour.Initialize();
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
            _levelTraverserBehaviour.Tick(deltaTime);
            _playerHandsBehaviour.Tick(deltaTime);
        }
    }
}