using UnityEngine;

namespace RogueDungeon.UI.LoadingScreen
{
    public abstract class View : MonoBehaviour, IView
    {
        public virtual void Discard() => 
            Destroy(gameObject);
    }
}