using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class RaycastTest : MonoBehaviour
    {
        private readonly List<RaycastResult> raycastResults = new();
        
        private void Update()
        {
            if(!Input.GetMouseButtonDown(0))
                return;
            
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = Input.mousePosition}, raycastResults);
            foreach (var raycastResult in raycastResults) 
                Debug.Log(raycastResult.gameObject.name);
        }
    }
}