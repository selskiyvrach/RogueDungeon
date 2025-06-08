using Zenject;

namespace Game.Libs.Items.Factory
{
    public class ItemFactory : IFactory<string, IItem>
    {
        private readonly IItemConfigsRepository _configsRepository;
        private readonly DiContainer _container;

        public ItemFactory(IItemConfigsRepository configsRepository, DiContainer container)
        {
            _configsRepository = configsRepository;
            _container = container;
        }

        public IItem Create(string id)
        {
            var config = _configsRepository.GetItemConfig(id);
            return (IItem)_container.Instantiate(config.Type, new object[]{config});
        }
    }
}