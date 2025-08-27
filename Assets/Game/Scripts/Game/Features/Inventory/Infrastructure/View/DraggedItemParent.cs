using Game.Features.Inventory.App.Presenters;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class DraggedItemParent : MonoBehaviour, IDraggedItemParent
    {
        private IDraggableArea _draggableArea;
        private IItemView _item;
        
        public Vector3 WorldPosition => transform.position;
        
        [Inject]
        public void Construct(IDraggableArea draggableArea) => 
            _draggableArea = draggableArea;

        public void SetItem(IItemView item)
        {
            Assert.IsNull(_item);
            _item = item;
            _item.SetParent(transform);
        }

        public void SetScreenPosition(Vector2 position) => 
            transform.position = _draggableArea.ScreenToWorldPoint(position);

        public void RemoveItem()
        {
            Assert.IsNotNull(_item);
            _item.SetParent(null);
            _item = null;
        }
    }
}