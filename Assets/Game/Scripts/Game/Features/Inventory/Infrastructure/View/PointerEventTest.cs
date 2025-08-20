using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class PointerEventTest : MonoBehaviour
    {
        private void OnMouseEnter() => 
            Debug.Log(gameObject.name + " enter");

        private void OnMouseExit() => 
            Debug.Log(gameObject.name + " exit");
    }
}