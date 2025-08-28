using System;
using System.Linq;
using Game.Features.Inventory.Domain;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class ContainerPresenter : IInitializable, IDisposable
    {
        public IContainerView View {get;}
        public ItemContainer Model {get;}
        
        private readonly IItemsViewProvider _itemsViewProvider;
        private readonly IPresentersRegistry _registry;

        public ContainerPresenter(IContainerView view, ItemContainer model, IItemsViewProvider itemsViewProvider, IPresentersRegistry registry)
        {
            View = view;
            Model = model;
            _itemsViewProvider = itemsViewProvider;
            _registry = registry;
        }

        public void Initialize()
        {
            Model.OnContentChanged += UpdateView;
            UpdateView();
            
            _registry.Register(this);
        }

        public void Dispose() => 
            _registry.Unregister(this);

        public ProjectionData GetProjection(IItem item, Camera camera, Vector2 pointerScreenPos)
        {
            var localPos = View.ScreenPosToLocalPosNormalized(pointerScreenPos, camera);
            var prospect = Model.GetItemPlacementProspect(item, localPos);
            var worldPos = View.LocalPosNormalizedToWorldPos(prospect.PosNormalized);
            return new ProjectionData(prospect, worldPos);
        }

        private void UpdateView()
        {
            foreach (var item in Model.GetItems()) 
                View.PlaceItem(_itemsViewProvider.GetView(item.item), item.posNormalized);
        }

        public void ExtractItem(ItemPresenter itemPresenter, out Vector2 placement)
        {
            placement = Model.GetItems().First(n => n.item == itemPresenter.Model).posNormalized;
            Model.RemoveItem(itemPresenter.Model);
        }
        
        public void RemoveItem(IItem item) => 
            Model.RemoveItem(item);
    }
}