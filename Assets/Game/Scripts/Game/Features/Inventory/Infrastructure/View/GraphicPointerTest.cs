using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class GraphicPointerTest : MonoBehaviour,
        IPointerEnterHandler,   
        IPointerExitHandler,    
        IPointerDownHandler,
        IPointerUpHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f, .75f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f,.5f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GetComponent<Image>().color = new Color(.5f, 1f, 1f,1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerEnter(eventData);
        }
    }
}