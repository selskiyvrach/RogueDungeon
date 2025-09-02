using System.Collections.Generic;
using Game.Features.Inventory.App.Presenters;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class ItemsViewCachedProvider : IItemsViewProvider
    {
        private readonly IFactory<IItem, IItemView> _factory;
        private readonly Dictionary<string, IItemView> _cachedViews = new();

        public ItemsViewCachedProvider(IFactory<IItem, IItemView> factory) => 
            _factory = factory;

        public IItemView GetView(IItem model)
        {
            if (_cachedViews.TryGetValue(model.Id, out var cached))
            {
                // checking for destroyed unity objects
                if (cached is not Object gameObject || gameObject != null)
                    return cached;
                _cachedViews.Remove(model.Id);
            }

            var created = _factory.Create(model);
            _cachedViews[model.Id] = created;
            return created;
        }
    }
}