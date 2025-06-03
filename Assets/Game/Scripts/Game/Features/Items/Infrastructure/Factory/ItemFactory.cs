using Game.Features.Items.Domain;
using Game.Features.Items.Infrastructure.Repository;
using Zenject;

namespace Game.Features.Items.Infrastructure.Factory
{
    public class ItemFactory : IFactory<string, IItem>
    {
        private readonly IItemConfigsRepository _configsRepository;
        private readonly DiContainer _container;

        public ItemFactory(DiContainer container, IItemConfigsRepository configsRepository)
        {
            _container = container;
            _configsRepository = configsRepository;
        }

        public IItem Create(string itemId)
        {
            var config = _configsRepository.GetItemConfig(itemId);
            var itemContainer = _container.CreateSubContainer();
            var item = itemContainer.Instantiate(((ItemConfig)config).ItemType, new object[]{config, itemContainer});
            return (IItem)item;
        }
    }
}