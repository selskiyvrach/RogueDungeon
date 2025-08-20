using System;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class ItemPresenter : IInitializable, IDisposable
    {
        private readonly IItem _model;
        private readonly IItemView _view;
        private readonly Mediator _hoverListener;
        private readonly ElementsRegistry _registry;

        public ItemPresenter(IItem model, IItemView view, Mediator hoverListener, ElementsRegistry registry)
        {
            _model = model;
            _view = view;
            _hoverListener = hoverListener;
            _registry = registry;
        }

        public void Initialize()
        {
            _registry.Register(this);
            _view.Setup(new ItemInfo(_model.Id, GetSprite(_model.TypeId), _model.Size));
            _view.OnHovered += HandleHovered;
            _view.OnUnhovered += HandleUnhovered;
        }

        private Sprite GetSprite(string modelTypeId)
        {
            throw new NotImplementedException();
        }

        private void HandleHovered() => 
            _hoverListener.OnItemHovered(this);

        private void HandleUnhovered() => 
            _hoverListener.OnItemUnhovered(this);

        public void Dispose()
        {
            _view.OnHovered -= HandleHovered;
            _view.OnUnhovered -= HandleUnhovered;
            _registry.Unregister(this);
        }

        public void DisableRaycasts() => 
            _view.Raycastable = false;
        
        public void EnableRaycasts() =>
            _view.Raycastable = true;

        public void DisplayHovered() => 
            _view.DisplayHovered();

        public void DisplayUnhovered() => 
            _view.DisplayUnhovered();

        public void DisplayBeingDragged() => 
            _view.DisplayBeingDragged();

        public void DisplayNotBeingDragged() => 
            _view.DisplayNotBeingDragged();
    }
}