using RogueDungeon.Items;
using RogueDungeon.Player.Model.Attacks;
using UnityEngine.Assertions;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandBehaviour : ITickable
    {
        private readonly HandheldItemFactory _factory;

        private IHandheldItem _currentItem;
        private IHandheldItem _intendedItem;
        public bool IsLocked { get; set; }

        public IHandheldItem CurrentItem
        {
            get => _currentItem;
            set
            {
                Assert.IsFalse(IsLocked);
                if(IsLocked)
                    return;
                
                if (value != null)
                {
                    _factory.Create(value);
                    value.Moveset.Initialize();
                }
                else
                    _factory.Destroy(_currentItem);
                
                _currentItem = value;
            }
        }

        public IHandheldItem IntendedItem
        {
            get => _intendedItem;
            set
            {
                Assert.IsFalse(IsLocked);
                if(!IsLocked)
                    _intendedItem = value;
            }
        }

        public bool IsIdleOrEmpty => _currentItem == null || _currentItem.Moveset.CurrentState is ItemIdleMove;

        public PlayerHandBehaviour(HandheldItemFactory factory) => 
            _factory = factory;

        public void Tick(float deltaTime)
        {
            _currentItem?.Moveset?.Tick(deltaTime);
            if(CurrentItem == null && IntendedItem != null)
                CurrentItem = IntendedItem; 
        }
    }
}