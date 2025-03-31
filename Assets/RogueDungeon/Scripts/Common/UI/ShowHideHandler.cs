using UnityEngine;

namespace Common.UI
{
    public class ShowHideHandler : MonoBehaviour
    {
        public bool IsShown { get; protected set; }
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
            IsShown = true;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            IsShown = false;
        }
    }
}