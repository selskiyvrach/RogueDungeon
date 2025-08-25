using System;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class ItemPresenter : IInitializable, IDisposable
    {
        private readonly Mediator _hoverListener;
        private readonly ElementsRegistry _registry;
        
        public IItem Model { get; }
        public IItemView View { get; }

        public ItemPresenter(IItem model, IItemView view, Mediator hoverListener, ElementsRegistry registry)
        {
            Model = model;
            View = view;
            _hoverListener = hoverListener;
            _registry = registry;
        }

        public void Initialize()
        {
            _registry.Register(this);
            View.Setup(new ItemInfo(Model.Id, GetSprite(Model.TypeId), Model.Size));
            View.OnHovered += HandleHovered;
            View.OnUnhovered += HandleUnhovered;
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
            View.OnHovered -= HandleHovered;
            View.OnUnhovered -= HandleUnhovered;
            _registry.Unregister(this);
        }

        public void DisableRaycasts() => 
            View.Raycastable = false;
        
        public void EnableRaycasts() =>
            View.Raycastable = true;

        public void DisplayHovered() => 
            View.DisplayHovered();

        public void DisplayUnhovered() => 
            View.DisplayUnhovered();

        public void DisplayBeingDragged() => 
            View.DisplayBeingDragged();

        public void DisplayNotBeingDragged() => 
            View.DisplayNotBeingDragged();
    }
}