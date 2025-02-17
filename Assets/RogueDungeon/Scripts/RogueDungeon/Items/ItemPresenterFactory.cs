using UnityEngine;
using Zenject;

namespace RogueDungeon.Items
{
    public class ItemPresenterFactory : IFactory<ItemConfig, HandHeldItemPresenter>
    {
        private readonly Transform _itemParent;

        public ItemPresenterFactory(Transform itemParent) => 
            _itemParent = itemParent;

        public HandHeldItemPresenter Create(ItemConfig param) => 
            Object.Instantiate(param.Prefab, _itemParent, worldPositionStays: false);
    }
}