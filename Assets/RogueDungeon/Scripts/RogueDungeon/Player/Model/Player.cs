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
    // player modes
        // hands + movement
        // inventory + movement (sit action)
        // map + movement
        
    // player actions
        // open map
            // create map item in the right hand as an intentional one
            // save previous item
            // when map removed - set the previous one as the intended one
            // when use button pressed - gets the map closer
        // open inventory
            // maybe create a two-handed item as well?
        
    // movement - sit down action, keep sitting, stand up
        // sitting down + laying down the bag
    
    
    public class Player : IInitializable, ITickable
    {
        private readonly PlayerPositionInTheMaze _mazeTraversalPointer;
        private readonly Level _level;
        private readonly IPlayerInput _input;
        
        private PlayerBehaviour Movement;
        private IItem _previousRightHandItem;

        public PlayerConfig Config { get; }
        public PlayerHandsBehaviour Hands { get; private set; }
        public Transform CameraPovPoint { get; }
        public Resource Health { get; }
        public RechargeableResource Stamina { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsHoldingBreath { get; set; }
        public bool IsAlive => Health.Current > 0;
        public float DoubleGripDamageBonus => Config.DoubleGripDamageBonus;
        public float DoubleGripBlockBonus => Config.DoubleGripBlockBonus;
        public bool HasUnabsorbedBlockImpact { get; set; }
        public IBlockingItem BlockingItem { get; set; }
        public float DodgeStaminaCost => Config.DodgeStaminaCost;

        public Player(PlayerConfig config, PlayerGameObject gameObject, Level level, PlayerPositionInTheMaze playerMazePosition, IPlayerInput input)
        {
            Config = config;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            _input = input;
            CameraPovPoint = gameObject.CameraReferencePoint;
            Stamina = new RechargeableResource(Config.Stamina);
            Health = new Resource(Config.Health);
            Stamina.Refill();
            Health.Refill();
        }

        public void SetBehaviours(PlayerHandsBehaviour handsBehaviour, PlayerBehaviour behaviour)
        {
            Hands = handsBehaviour;
            Movement = behaviour;   
        }

        public void Initialize()
        {
            _level.LevelTraverser = _mazeTraversalPointer;
            Movement.Initialize();
            Hands.Initialize();
        }

        public void Tick(float deltaTime)
        {
            if (IsAlive)
            {
                if (_input.IsDown(InputKey.CycleLeftArmItems))
                {
                    Hands.LeftHand.IntendedItem = Hands.LeftHand.IntendedItem != null ? null : new Weapon(Config.DefaultWeapon);
                    _input.ConsumeInput(InputKey.CycleLeftArmItems);
                }
                if (_input.IsDown(InputKey.CycleRightArmItems))
                {
                    Hands.RightHand.IntendedItem = Hands.RightHand.IntendedItem != null ? null : new Shield(Config.DefaultShield);
                    _input.ConsumeInput(InputKey.CycleRightArmItems);
                }
                if (_input.IsDown(InputKey.OpenMap) && Hands.RightHand.CurrentItem is not Map)
                {
                    _previousRightHandItem = Hands.RightHand.CurrentItem;
                    Hands.RightHand.IntendedItem = new Map(Config.MapItemConfig);
                    _input.ConsumeInput(InputKey.OpenMap);
                }
                else if (_input.IsDown(InputKey.OpenMap) && Hands.RightHand.CurrentItem is Map)
                {
                    Hands.RightHand.IntendedItem = _previousRightHandItem;
                    _input.ConsumeInput(InputKey.OpenMap);
                }
            }

            if(IsAlive)
                Stamina.Tick(deltaTime);
            Movement.Tick(deltaTime);
            if(IsAlive)
                Hands.Tick(deltaTime);
        }
    }
}