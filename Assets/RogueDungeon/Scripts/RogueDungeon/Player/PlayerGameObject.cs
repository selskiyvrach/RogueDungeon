using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerGameObject : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraReferencePoint { get; private set; }
    }
}