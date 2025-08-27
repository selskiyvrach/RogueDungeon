using System;
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
        
        private readonly IFactory<IItem, IItemView> _itemFactory;
        private readonly IPresentersRegistry _registry;

        public ContainerPresenter(IContainerView view, ItemContainer model, IFactory<IItem, IItemView> itemFactory, IPresentersRegistry registry)
        {
            View = view;
            Model = model;
            _itemFactory = itemFactory;
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
            var placement = Model.GetItemPlacementInquiry(new ItemPlacementProposition(localPos.x, localPos.y, item));
            var worldPos = View.LocalPosNormalizedToWorldPos(localPos);
            return new ProjectionData(placement, worldPos);
        }

        private void UpdateView()
        {
            View.Reset();
            foreach (var item in Model.GetItems()) 
                View.PlaceItem(_itemFactory.Create(item.item), item.posNormalized);
        }

        public void PlaceItem(ItemPresenter item, ItemPlacementInquiryResult placementInquiryResult) => 
            Model.PlaceItem(item.Model, placementInquiryResult);

        public void ExtractItem(ItemPresenter itemPresenter) => 
            Model.ExtractItem(itemPresenter.Model.Id);
    }
}