using UnityEngine;

namespace RogueDungeon.Player.Model
{
    public class PlayerGameObject : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraReferencePoint { get; private set; }
    }
}