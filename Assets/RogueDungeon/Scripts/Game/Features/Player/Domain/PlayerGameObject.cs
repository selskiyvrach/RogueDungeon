using UnityEngine;

namespace Game.Features.Player.Domain
{
    public class PlayerGameObject : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraReferencePoint { get; private set; }
    }
}