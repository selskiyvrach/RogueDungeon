using UnityEngine;

namespace Common.Camera
{
    public interface IGameCamera
    {
        Transform Follow { get; set; }
    }
}