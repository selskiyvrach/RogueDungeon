using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class RectTest : MonoBehaviour
    {
        private void Update()
        {
            var rectTransform = GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition,
                Camera.main, out var pos);
            Debug.Log((pos + rectTransform.sizeDelta / 2) / rectTransform.sizeDelta);
        }
    }
}