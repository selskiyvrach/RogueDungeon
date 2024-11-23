using UnityEngine;

namespace RogueDungeon.Camera
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        [field: SerializeField] public UnityEngine.Camera Camera { get; private set; }
        [field: SerializeField] public Transform Follow { get; set; }
    }
}