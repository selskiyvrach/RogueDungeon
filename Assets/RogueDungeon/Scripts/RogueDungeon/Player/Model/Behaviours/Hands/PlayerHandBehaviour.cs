using Common.Fsm;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Attacks;
using UnityEngine.XR;
using Zenject;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandBehaviour : ITickable
    {
        private readonly HandheldItemFactory _factory;

        private IItem _currentItem;
        private IItem _intendedMainHandItem;

        public IItem CurrentItem
        {
            get => _currentItem;
            set
            {
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
        public IItem IntendedItem { get; set; }

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