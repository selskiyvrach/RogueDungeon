using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.App.UseCases;
using Game.Libs.UI;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class InventoryScreenSpawner : IScreen, IInitializable
    {
        private readonly IFactory<Transform, InventoryPresenter> _inventoryViewFactory;
        private readonly Transform _parent;
        private readonly IScreensRegistry _screensRegistry;
        
        private InventoryPresenter _instance;

        public InventoryScreenSpawner(IFactory<Transform, InventoryPresenter> inventoryViewFactory, Transform parent, IScreensRegistry screensRegistry)
        {
            _inventoryViewFactory = inventoryViewFactory;
            _parent = parent;
            _screensRegistry = screensRegistry;
        }

        public void Initialize() => 
            _screensRegistry.Register(this);

        public bool AcceptsShowRequest(IShowRequest request) => 
            request is ShowInventoryRequest;

        public bool AcceptsHideRequest(IHideRequest request) => 
            request is HideInventoryRequest;

        public void Show(IShowRequest request)
        {
            Assert.IsNull(_instance);
            _instance = _inventoryViewFactory.Create(_parent);
        }

        public void Hide(IHideRequest request)
        {
            Assert.IsNotNull(_instance);
            _instance.Dispose();
            _instance = null;
        }
    }
}