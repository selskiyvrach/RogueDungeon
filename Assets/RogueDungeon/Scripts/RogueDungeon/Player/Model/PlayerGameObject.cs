using UnityEngine;

namespace Player.Model
{
    public class PlayerGameObject : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraReferencePoint { get; private set; }
    }
}