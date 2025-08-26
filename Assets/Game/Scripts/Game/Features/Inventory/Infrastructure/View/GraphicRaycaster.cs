using System.Collections.Generic;
using System.Linq;
using Game.Features.Inventory.App.Presenters;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class GraphicRaycaster : IGraphicRaycaster
    {
        private readonly List<RaycastResult> raycastResults = new(10);
        private readonly PointerEventData _eventData;
        private readonly EventSystem _eventSystem;

        public GraphicRaycaster(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
            _eventData = new PointerEventData(eventSystem);
        }

        public IEnumerable<T> RaycastAll<T>(Vector2 screenPoint)
        {
            _eventData.Reset();
            _eventData.position = screenPoint;
            _eventSystem.RaycastAll(_eventData, raycastResults);
            return raycastResults.Select(n => n.gameObject.GetComponent<T>()).Where(n => n != null);
        }
    }
}