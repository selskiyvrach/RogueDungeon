using Game.Features.Inventory.App.UseCases;
using Game.Libs.UI;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class InventoryScreenSpawner : IScreen, IInitializable
    {
        private readonly IFactory<Transform, GameObject> _inventoryViewFactory;
        private readonly Transform _parent;
        private readonly IScreensRegistry _screensRegistry;

        private GameObject _screen;
        
        public InventoryScreenSpawner(IFactory<Transform, GameObject> inventoryViewFactory, Transform parent, IScreensRegistry screensRegistry)
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
            Assert.IsNull(_screen);
            _screen = _inventoryViewFactory.Create(_parent);
        }

        public void Hide(IHideRequest request)
        {
            Assert.IsNotNull(_screen);
            Object.Destroy(_screen);
            _screen = null;
        }
    }
}