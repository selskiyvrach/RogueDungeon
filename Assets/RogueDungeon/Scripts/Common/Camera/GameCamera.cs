using UnityEngine;

namespace Common.Camera
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        [field: SerializeField] public UnityEngine.Camera Camera { get; private set; }
        [field: SerializeField] public Transform Follow { get; set; }

        private void LateUpdate()
        {
            if (Follow == null) return;
            transform.rotation = Follow.rotation;
            transform.position = Follow.position;
        }
    }
}