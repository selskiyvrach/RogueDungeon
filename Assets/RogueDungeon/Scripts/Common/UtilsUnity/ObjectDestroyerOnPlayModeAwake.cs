using UnityEngine;

namespace Common.UtilsUnity
{
    public class ObjectDestroyerOnPlayModeAwake : MonoBehaviour
    {
        private void Awake() => 
            Destroy(gameObject);
    }
}