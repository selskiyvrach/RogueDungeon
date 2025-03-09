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
        private readonly IPlayerMovementBehaviour _levelTraverserBehaviour;
        private readonly PlayerConfig _config;
        private readonly PlayerHandsBehaviour _playerHandsBehaviour;
        private readonly Level _level;
        public Transform CameraPovPoint { get; }
        public PlayerPositionInTheMaze WorldObject { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsAlive { get; }

        public Player(PlayerConfig config, PlayerHandsBehaviour playerHandsBehaviour, PlayerGameObject gameObject,
            IPlayerMovementBehaviour levelTraverserBehaviour, Level level, PlayerPositionInTheMaze playerMazePosition)
        {
            _config = config;
            _playerHandsBehaviour = playerHandsBehaviour;
            _levelTraverserBehaviour = levelTraverserBehaviour;
            _level = level;
            WorldObject = playerMazePosition;
            CameraPovPoint = gameObject.CameraReferencePoint;
        }

        public void Initialize()
        {
            WorldObject.LocalPosition = Vector3.zero;
            WorldObject.Rotation = Vector2.down;
            _level.LevelTraverser = WorldObject;
            _levelTraverserBehaviour.Initialize();
            _playerHandsBehaviour.Initialize();
            ((IHandheldContext)_playerHandsBehaviour).IntendedItem = new Item(_config.DefaultWeapon); 
        }

        public void TakeDamage(float damage) => 
            Debug.LogError("Player taking damage");

        public void Tick(float deltaTime)
        {
            _levelTraverserBehaviour.Tick(deltaTime);
            _playerHandsBehaviour.Tick(deltaTime);
        }
    }
}