using System;
using Characters;
using Common.Lifecycle;
using Input;
using Inventory.Shared;
using Levels;
using Player.Model.Behaviours.Common;
using Player.Model.Behaviours.Hands;
using RogueDungeon.Items.Model;
using UnityEngine;

namespace Player.Model
{
    public class Player : IInitializable, ITickable
    {
        private readonly PlayerPositionInTheMaze _mazeTraversalPointer;
        private readonly Level _level;
        private readonly IPlayerInput _input;
        
        private PlayerBehaviour Movement;
        private IHandheldItem _previousRightHandItem;

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
        public Inventory.Model.Inventory Inventory { get; }
        public event Action OnShowInventoryRequested;

        public Player(PlayerConfig config, PlayerGameObject gameObject, Level level, PlayerPositionInTheMaze playerMazePosition, IPlayerInput input, Inventory.Model.Inventory inventory)
        {
            Config = config;
            _level = level;
            _mazeTraversalPointer = playerMazePosition;
            _input = input;
            Inventory = inventory;
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
            Inventory.Equip(new Weapon(Config.DefaultWeapon), SlotType.HandheldLeft1);
            Inventory.Equip(new Shield(Config.DefaultShield), SlotType.HandheldLeft2);
            Inventory.Equip(new Weapon(Config.DefaultWeapon), SlotType.HandheldRight1);
            Inventory.Equip(new Shield(Config.DefaultShield), SlotType.HandheldRight2);
            Hands.Initialize();
            Hands.Enable();
        }

        public void Tick(float deltaTime)
        {
            Movement.Tick(deltaTime);

            if (!IsAlive)
                return;

            Hands.Tick(deltaTime);
            Stamina.Tick(deltaTime);
            
            if (_input.GetKey(InputKey.OpenMap).IsDown && Hands.RightHand.CurrentItem is not Map)
            {
                _previousRightHandItem = Hands.RightHand.CurrentItem;
                Hands.RightHand.IntendedItem = new Map(Config.MapItemConfig);
                _input.GetKey(InputKey.OpenMap).Reset();
            }
            else if (_input.GetKey(InputKey.OpenMap).IsDown && Hands.RightHand.CurrentItem is Map)
            {
                Hands.RightHand.IntendedItem = _previousRightHandItem;
                _input.GetKey(InputKey.OpenMap).Reset();
            }
        }

        public void ShowInventory()
        {
            OnShowInventoryRequested.Invoke();
        }
    }
}