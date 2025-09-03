using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Infrastructure.View;
using Game.Libs.Items;
using Libs.Utils.DotNet;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class ItemViewFactory : IFactory<IItem, IItemView>
    {
        private readonly DiContainer _container;
        private readonly ItemView _prefab;
        private readonly IItemConfigsRepository _configsRepository;

        public ItemViewFactory(DiContainer container, ItemView prefab, IItemConfigsRepository configsRepository)
        {
            _container = container;
            _prefab = prefab;
            _configsRepository = configsRepository;
        }

        public IItemView Create(IItem param1)
        {
            param1.ThrowIfNull();
            _container.Bind<IItem>().FromInstance(param1).AsCached();
            var view = _container.InstantiatePrefabForComponent<ItemView>(_prefab);
            _container.Unbind<IItem>();
            view.gameObject.name = $"item_{param1.Id}";
            view.Setup(new ItemViewSetupArgs(param1.Id, _configsRepository.GetItemSprite(param1.TypeId), param1.Size));
            return view;
        }
    }
}