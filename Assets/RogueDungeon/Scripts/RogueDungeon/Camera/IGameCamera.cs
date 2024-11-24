using UnityEngine;

namespace RogueDungeon.Camera
{
    public interface IGameCamera
    {
        Transform Follow { get; set; }
    }
}