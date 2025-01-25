using UnityEngine;

namespace Common.Unity
{
    public class ObjectDestroyerOnPlayModeAwake : MonoBehaviour
    {
        private void Awake() => 
            Destroy(gameObject);
    }
}