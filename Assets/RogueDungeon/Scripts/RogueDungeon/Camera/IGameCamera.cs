using UnityEngine;

namespace RogueDungeon.Camera
{
    public interface IGameCamera
    {
        Transform Follow { get; set; }
        void KickPosition(Vector3 punch, float duration);
    }
}