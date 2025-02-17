using RogueDungeon.Items;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class PlayerHands : IHandheldContext
    {
        private readonly IFactory<ItemConfig, HandHeldItemPresenter> _presenterFactory;
        private Item _currentItem;
        private HandHeldItemPresenter _presenter;

        public Item IntendedItem { get; set; }

        public Item CurrentItem
        {
            get => _currentItem;
            set
            {
                _currentItem = value;
                if (_currentItem != null)
                    _presenter = _presenterFactory.Create(_currentItem.Config);
                else
                {
                    _presenter.Destroy();
                    _presenter = null;
                }
            }
        }

        public void SetCurrentItemInteractable(bool value)
        {
            // what entity would control behaviours???
        }
    }
}