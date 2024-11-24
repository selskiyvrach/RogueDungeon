using UnityEngine;

namespace RogueDungeon.UI.Common
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasSorter : MonoBehaviour
    {
        [SerializeField] private UiSortingOrder uiSortingOrder;
        [SerializeField, HideInInspector] private Canvas _canvas;

        private void OnValidate()
        {
            _canvas ??= GetComponent<Canvas>();
            if (_canvas.sortingOrder != (int)uiSortingOrder)
                _canvas.sortingOrder = (int)uiSortingOrder;
        }
    }
}