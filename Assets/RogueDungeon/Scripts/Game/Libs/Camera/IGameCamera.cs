using UnityEngine;

namespace Game.Libs.Camera
{
    public interface IGameCamera
    {
        UnityEngine.Camera Camera { get; }
        Transform Follow { get; set; }
        Ray MouseRay { get; }
        void KickPosition(Vector3 punch, float duration);
    }
}