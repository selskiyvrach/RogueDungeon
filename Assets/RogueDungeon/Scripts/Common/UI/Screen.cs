using UnityEngine;

namespace Common.UI
{
    [RequireComponent(typeof(Canvas))]
    public class Screen : UiElement
    {
        [SerializeField, HideInInspector] protected Canvas Canvas;

        protected virtual void OnValidate() => 
            Canvas ??= GetComponent<Canvas>();

        public int SortingOrder => Canvas.sortingOrder;
    }
}