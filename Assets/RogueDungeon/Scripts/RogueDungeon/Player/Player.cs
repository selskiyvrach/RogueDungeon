using Common.Lifecycle;
using Common.Unity;
using RogueDungeon.Items;
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
        public Transform CameraPovPoint { get; }
        public TwoDWorldObject WorldObject { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsAlive { get; }

        public Player(PlayerConfig config, PlayerHandsBehaviour playerHandsBehaviour, PlayerGameObject gameObject, IPlayerMovementBehaviour levelTraverserBehaviour)
        {
            _config = config;
            _playerHandsBehaviour = playerHandsBehaviour;
            _levelTraverserBehaviour = levelTraverserBehaviour;
            WorldObject = new TwoDWorldObject(gameObject.gameObject);
            CameraPovPoint = gameObject.CameraReferencePoint;
            _levelTraverserBehaviour.ObjectToMove = WorldObject;
        }

        public void Initialize()
        {
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