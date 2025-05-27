using Inventory.View;
using RogueDungeon.Items.Model;
using Zenject;

namespace Inventory.Presenter
{
    public class WorldInventoryItemFactory : IFactory<IItem, InventoryItemView>
    { 
        private readonly DiContainer _container;

        public WorldInventoryItemFactory(DiContainer container) => 
            _container = container;

        public InventoryItemView Create(IItem item)
        {
            var container = _container.CreateSubContainer();
            // container.InstanceSingle(param1);
            // return container.InstantiatePrefab(param1.Config.WorldInventoryPrefab).GetComponent<ItemView>();
            return null;
        }
    }
}