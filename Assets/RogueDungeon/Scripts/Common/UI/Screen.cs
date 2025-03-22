using UnityEngine;

namespace Common.UI
{
    public class Screen : UiElement
    {
        [SerializeField] private Canvas _canvas;
        public int SortingOrder => _canvas.sortingOrder;
    }
}