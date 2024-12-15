using UnityEngine;

namespace Common.UnityUtils
{
    public class ObjectDestroyerOnPlayModeAwake : MonoBehaviour
    {
        private void Awake() => 
            Destroy(gameObject);
    }
}