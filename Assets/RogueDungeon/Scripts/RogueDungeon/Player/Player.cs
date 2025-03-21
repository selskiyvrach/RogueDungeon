using Common.Lifecycle;
using RogueDungeon.Characters;
using RogueDungeon.Items;
using RogueDungeon.Levels;
using RogueDungeon.Player.Behaviours.Common;
using RogueDungeon.Player.Behaviours.Hands;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class Player : IInitializable, ITickable
    {
        private readonly PlayerConfig _config;
        private readonly PlayerPositionInTheMaze _mazeTraversalPointer;
        private readonly Level _level;
        
        private PlayerCommonBehaviour _commonBehaviour;
        private PlayerHandsBehaviour _playerHandsBehaviour;

        public Transform CameraPovPoint { get; }
        public PlayerBlockerHandler BlockerHandler { get; }
        public Resource Health { get; }
        public RechargeableResource Stamina { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsAlive => Health.Current > 0;

        public Player(PlayerConfig config, PlayerGameObject gameObject, Level level, PlayerPositionInTheMaze playerMazePosition)
        {
            _config = config;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            BlockerHandler = new PlayerBlockerHandler(this);
            CameraPovPoint = gameObject.CameraReferencePoint;
            Stamina = new RechargeableResource(_config.Stamina);
            Health = new Resource(_config.Health);
            Stamina.Refill();
            Health.Refill();
        }

        public void SetBehaviours(PlayerHandsBehaviour handsBehaviour, PlayerCommonBehaviour commonBehaviour)
        {
            _playerHandsBehaviour = handsBehaviour;
            _commonBehaviour = commonBehaviour;   
        }

        public void Initialize()
        {
            _level.LevelTraverser = _mazeTraversalPointer;
            _commonBehaviour.Initialize();
            _playerHandsBehaviour.Initialize();
            ((IHandheldContext)_playerHandsBehaviour).IntendedItem = new Weapon(_config.DefaultWeapon);
        }

        public void TakeHitDamage(float damage) => 
            Health.AddDelta(-damage);

        public void Tick(float deltaTime)
        {
            if(IsAlive)
                Stamina.Tick(deltaTime);
            _commonBehaviour.Tick(deltaTime);
            if(IsAlive)
                _playerHandsBehaviour.Tick(deltaTime);
        }
    }
}