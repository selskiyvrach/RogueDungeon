using UnityEngine;

namespace Libs.Utils.Unity
{
    public class ObjectDestroyerOnPlayModeAwake : MonoBehaviour
    {
        private void Awake() => 
            Destroy(gameObject);
    }
}