using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Infrastructure.View;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class ItemFactory : IFactory<IItem, IItemView>
    {
        private readonly DiContainer _container;
        private readonly ItemView _prefab;
        private readonly IItemConfigsRepository _configsRepository;

        public ItemFactory(DiContainer container, ItemView prefab, IItemConfigsRepository configsRepository)
        {
            _container = container;
            _prefab = prefab;
            _configsRepository = configsRepository;
        }

        public IItemView Create(IItem param1)
        {
            var view = _container.InstantiatePrefab(_prefab).GetComponent<ItemView>();
            view.Setup(new ItemInfo(param1.Id, _configsRepository.GetItemSprite(param1.TypeId), param1.Size));
            return view;
        }
    }
}