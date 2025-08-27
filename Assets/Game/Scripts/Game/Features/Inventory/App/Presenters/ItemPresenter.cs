using System;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public sealed class ItemPresenter : IInitializable, IDisposable
    {
        private readonly IPresentersRegistry _registry;

        private bool _isInitialized;
        private bool _isDragging;

        public IItem Model { get; }
        public IItemView View { get; }
        public IProjectionView Projection => View.ProjectionView;

        public ItemPresenter(IItem model, IItemView view, IPresentersRegistry registry)
        {
            Model = model;
            View = view;
            _registry = registry;
        }

        public void Initialize()
        {
            if (_isInitialized) 
                throw new InvalidOperationException();
            _registry.Register(this);
            _isInitialized = true;
        }

        public void Dispose()
        {
            if (!_isInitialized) 
                throw new InvalidOperationException();

            _registry.Unregister(this);
            _isInitialized = false;
        }

        public void SetHovered(bool value)
        {
            if (_isDragging)
                throw new InvalidOperationException();
            
            if (View.IsHovered == value)
                throw new InvalidOperationException();
            
            View.DisplayHovered(value);
        }

        public void EnableRaycasts(bool value)
        {
            if(View.Raycastable != value)
                View.Raycastable = value;
        }

        public void OnPlacementDenied()
        {
            throw new NotImplementedException();
        }
    }
}