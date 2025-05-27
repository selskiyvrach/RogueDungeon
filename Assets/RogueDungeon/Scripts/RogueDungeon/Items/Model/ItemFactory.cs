using RogueDungeon.Items.Model.Configs;
using Zenject;

namespace RogueDungeon.Items.Model
{
    public class ItemFactory : IFactory<ItemConfig, IItem>
    {
        private readonly DiContainer _container;

        public ItemFactory(DiContainer container) => 
            _container = container;

        public IItem Create(ItemConfig config)
        {
            var itemContainer = _container.CreateSubContainer();
            var item = itemContainer.Instantiate(config.ItemType, new object[]{config, itemContainer});
            return (IItem)item;
        }
    }
}