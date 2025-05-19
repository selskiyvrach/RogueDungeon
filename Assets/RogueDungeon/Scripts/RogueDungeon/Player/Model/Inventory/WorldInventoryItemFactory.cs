using Common.UtilsZenject;
using RogueDungeon.Items;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventoryItemFactory : IFactory<IItem, ItemContainer, Vector3, ItemView>
    {
        private readonly DiContainer _container;
        private readonly ItemView _prefab;

        public WorldInventoryItemFactory(DiContainer container, ItemView prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public ItemView Create(IItem param1, ItemContainer param2, Vector3 param3)
        {
            var container = _container.CreateSubContainer();
            container.InstanceSingle(param1);
            container.InstanceSingle(param2);
            container.InstanceSingle(param3);
            return container.InstantiatePrefab(_prefab).GetComponent<ItemView>();
        }
    }
}