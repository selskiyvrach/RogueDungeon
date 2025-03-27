using Common.Fsm;
using RogueDungeon.Items;
using Zenject;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandBehaviour
    {
        private readonly IFactory<IItem, HandHeldItemPresenter> _presenterFactory;
        private readonly ItemMoveSetFactory _moveSetFactory;
        
        private IItem _currentItem;
        private IItem _intendedMainHandItem;
        
        private HandHeldItemPresenter _itemPresenter;
        private StateMachine _itemMoveSet;
        private StateMachine _unsheathMoveSet;

        public IItem CurrentItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }

        public IItem IntendedItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }

        public PlayerHandBehaviour(IFactory<IItem, HandHeldItemPresenter> presenterFactory, ItemMoveSetFactory moveSetFactory)
        {
            _presenterFactory = presenterFactory;
            _moveSetFactory = moveSetFactory;
        }

        public void SetUnsheathBehaviour(StateMachine unsheathMoveSet) => 
            _unsheathMoveSet = unsheathMoveSet;

        public void Initialize() => 
            _unsheathMoveSet.Initialize();

        public void SetCurrentItem(Item value)
        {
            _currentItem = value;
            if (_currentItem != null)
                _itemPresenter = _presenterFactory.Create(_currentItem);
            else
            {
                _itemPresenter.Release();
                _itemPresenter = null;
                DeleteItemMoveSet();
            }
        }

        public void SetItemMoveSetActive(bool value)
        {
            if(value)
                CreateItemMoveSet();
            else
                DeleteItemMoveSet();
        }

        private void CreateItemMoveSet()
        {
            _moveSetFactory.BindItem(_currentItem);
            _itemMoveSet = _moveSetFactory.Create(_currentItem.MoveSetConfig);
            _itemMoveSet.Initialize();
        }

        private void DeleteItemMoveSet()
        {
            _moveSetFactory.Unbind();
            _itemMoveSet = null;
        }

        public void Tick(float deltaTime)
        {
            _itemMoveSet?.Tick(deltaTime);
            _unsheathMoveSet.Tick(deltaTime);
        }
    }
}