using UnityEngine.Assertions;
using Zenject;

namespace RogueDungeon.Items
{
    public class ItemPresenterFactory : IFactory<ItemConfig, HandHeldItemPresenter>
    {
        private readonly HandHeldItemPresenter _itemPresenter;

        public ItemPresenterFactory(HandHeldItemPresenter itemPresenter) => 
            _itemPresenter = itemPresenter;

        public HandHeldItemPresenter Create(ItemConfig param)
        {
            Assert.IsTrue(_itemPresenter.IsReleased);
            _itemPresenter.IsReleased = false;
            _itemPresenter.Sprite = param.Sprite;
            return _itemPresenter;
        }
    }
}