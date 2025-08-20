using Libs.Utils.DotNet;
using Zenject;

namespace Game.Libs.Items.Factory
{
    public class ItemFactory : IFactory<string, IItem>
    {
        private readonly IItemConfigsRepository _configsRepository;
        private readonly DiContainer _container;
        private readonly IUniqueNameGenerator _nameGenerator;

        public ItemFactory(IItemConfigsRepository configsRepository, DiContainer container, IUniqueNameGenerator nameGenerator)
        {
            _configsRepository = configsRepository;
            _container = container;
            _nameGenerator = nameGenerator;
        }

        public IItem Create(string id)
        {
            var config = _configsRepository.GetItemConfig(id);
            var instanceId = _nameGenerator.GetUniqueName(id); 
            return (IItem)_container.Instantiate(config.Type, new object[]{config, instanceId});
        }
    }
}