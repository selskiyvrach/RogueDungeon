using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Libs.UI
{
    public class HoverableGraphic : RaycastableGraphic, IPointerEnterHandler, IPointerExitHandler, IHoverable
    {
        public event Action OnHovered;
        public event Action OnUnhovered;
        
        public void OnPointerEnter(PointerEventData eventData) => 
            OnHovered?.Invoke();

        public void OnPointerExit(PointerEventData eventData) => 
            OnUnhovered?.Invoke();
    }
}