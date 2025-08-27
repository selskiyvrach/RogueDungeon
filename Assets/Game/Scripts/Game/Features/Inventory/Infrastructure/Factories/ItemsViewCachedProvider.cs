using System.Collections.Generic;
using Game.Features.Inventory.App.Presenters;
using Game.Libs.Items;
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
            if(!_cachedViews.ContainsKey(model.Id))
                _cachedViews.Add(model.Id, _factory.Create(model));
            return _cachedViews[model.Id];
        }
    }
}