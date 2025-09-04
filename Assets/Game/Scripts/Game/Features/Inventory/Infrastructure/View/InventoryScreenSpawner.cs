using Game.Features.Inventory.App.UseCases;
using Game.Libs.UI;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class InventoryScreenSpawner : IScreen, IInitializable
    {
        private readonly IFactory<Transform, InventoryView> _inventoryViewFactory;
        private readonly Transform _parent;
        private readonly IScreensRegistry _screensRegistry;

        private InventoryView _screen;
        
        public InventoryScreenSpawner(IFactory<Transform, InventoryView> inventoryViewFactory, Transform parent, IScreensRegistry screensRegistry)
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
            _screen.Show();
        }

        public void Hide(IHideRequest request)
        {
            Assert.IsNotNull(_screen);
            _screen.Hide(() =>
            {
                _screen.Destroy();
                _screen = null;
            });
        }
    }
}