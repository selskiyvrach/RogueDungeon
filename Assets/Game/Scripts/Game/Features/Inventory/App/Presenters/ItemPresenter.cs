using System;
using Game.Libs.Items;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public sealed class ItemPresenter : IInitializable, IDisposable
    {
        private readonly IItemViewAnimator _animator;
        private readonly IPresentersRegistry _registry;

        private bool _isInitialized;
        private bool _isDisposed;
        private bool _isDragging;

        public IItem Model { get; }
        public IItemView View { get; }
        public IProjectionView Projection => View.ProjectionView;

        public ItemPresenter(IItem model, IItemView view, IItemViewAnimator animator, IPresentersRegistry registry)
        {
            Model = model;
            View = view;
            _animator = animator;
            _registry = registry;
        }

        public void Initialize()
        {
            if (_isInitialized) 
                throw new InvalidOperationException();
            _animator.OnAnimationFinished += View.RefreshSubElementsPositions;
            _registry.Register(this);
            _isInitialized = true;
        }

        public void Dispose()
        {
            if(_isDisposed)
                return;
            _registry.Unregister(this);
            _animator.OnAnimationFinished -= View.RefreshSubElementsPositions;
            View.Dispose();
            _isDisposed = true;
        }

        public void DisplaySelected(bool value) => 
            View.DisplayHovered(value);

        public void PlayDroppedAnimation() => 
            _animator.PlayDropped();
    }
}