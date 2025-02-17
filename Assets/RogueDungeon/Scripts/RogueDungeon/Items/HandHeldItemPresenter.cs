using UnityEngine;

namespace RogueDungeon.Items
{
    public class HandHeldItemPresenter : MonoBehaviour
    {
        public void Destroy() => 
            Destroy(gameObject);
    }
}