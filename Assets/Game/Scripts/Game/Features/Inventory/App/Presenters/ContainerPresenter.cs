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
        private readonly Mediator _mediator;
        private readonly IContainerView _view;
        private readonly ItemContainer _model;
        private readonly IFactory<IItem, IItemView> _itemFactory;

        public ContainerPresenter(IContainerView view, ItemContainer model, Mediator mediator, IFactory<IItem, IItemView> itemFactory)
        {
            _view = view;
            _model = model;
            _mediator = mediator;
            _itemFactory = itemFactory;
        }

        public void Initialize()
        {
            _model.OnContentChanged += UpdateView;
            UpdateView();
            
            _mediator.Registry.Register(this);
        }

        private void UpdateView()
        {
            _view.Reset();
            // register items
            // unregister items
            foreach (var item in _model.GetItems()) 
                _view.PlaceItem(_itemFactory.Create(item.item), item.posNormalized);
        }

        public void Dispose() => 
            _mediator.Registry.Unregister(this);

        public ProjectionData GetProjection(IItem item, Camera camera, Vector2 pointerScreenPos)
        {
            var localPos = _view.ScreenPosToLocalPosNormalized(pointerScreenPos, camera);
            var placement = _model.GetItemPlacement(new ItemPlacementProposition(localPos.x, localPos.y, item));
            var worldPos = _view.LocalPosNormalizedToWorldPos(localPos);
            return new ProjectionData(placement, worldPos);
        }
    }
}