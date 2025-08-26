using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class RaycastTest : MonoBehaviour
    {
        private readonly List<RaycastResult> raycastResults = new();
        private PointerEventData _pointerEventData;

        private void Awake() => 
            _pointerEventData = new PointerEventData(EventSystem.current);

        private void Update()
        {
            if(!Input.GetMouseButtonDown(0))
                return;
            
            _pointerEventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(_pointerEventData, raycastResults);
            foreach (var raycastResult in raycastResults) 
                Debug.Log(raycastResult.gameObject.name);
        }
    }
}