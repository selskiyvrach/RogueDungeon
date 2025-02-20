using Common.Behaviours;
using Common.MoveSets;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class PlayerHandsBehaviour : Behaviour, IHandheldContext
    {
        private readonly HandsArgs _args;
        
        private Item _currentItem;
        private Item _intendedItem;
        private HandHeldItemPresenter _itemPresenter;
        private MoveSetBehaviour _itemMoveSet;

        Item IHandheldContext.CurrentItem
        {
            get => _args.HandHeldContext.CurrentItem;
            set => _args.HandHeldContext.CurrentItem = value;
        }

        Item IHandheldContext.IntendedItem
        {
            get => _args.HandHeldContext.IntendedItem;
            set => _args.HandHeldContext.IntendedItem = value;
        }

        void IHandheldContext.SetCurrentItemInteractable(bool value) => 
            _args.HandHeldContext.SetCurrentItemInteractable(value);

        public PlayerHandsBehaviour(HandsArgs handsArgs) => 
            _args = handsArgs;

        public override void Enable()
        {
            if(!_args.HandHeldContext.Inited)
                _args.HandHeldContext.Init(() => _currentItem, () => _intendedItem, SetCurrentItem, n => _intendedItem = n, SetItemMoveSetActive);
            _args.UnsheathMoveSet.Enable();
            base.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            _args.UnsheathMoveSet.Disable();
        }

        private void SetCurrentItem(Item value)
        {
            _currentItem = value;
            if (_currentItem != null)
                _itemPresenter = _args.PresenterFactory.Create(_currentItem.Config);
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
            _itemMoveSet = _args.ItemMoveSetFactory.Create(_currentItem.Config.MoveSetConfig);
            _itemMoveSet.Enable();
        }

        private void DeleteItemMoveSet()
        {
            _itemMoveSet?.Disable();
            _itemMoveSet = null;
        }
    }
}