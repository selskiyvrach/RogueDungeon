using UnityEngine.Assertions;
using Zenject;

namespace RogueDungeon.Items
{
    public class ItemPresenterFactory : IFactory<IItem, HandHeldItemPresenter>
    {
        private readonly HandHeldItemPresenter _itemPresenter;

        public ItemPresenterFactory(HandHeldItemPresenter itemPresenter) => 
            _itemPresenter = itemPresenter;

        public HandHeldItemPresenter Create(IItem param)
        {
            Assert.IsTrue(_itemPresenter.IsReleased);
            _itemPresenter.IsReleased = false;
            _itemPresenter.Sprite = param.Config.Sprite;
            return _itemPresenter;
        }
    }
}