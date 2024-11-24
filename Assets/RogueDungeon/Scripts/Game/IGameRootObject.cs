using UnityEngine;

namespace RogueDungeon.Game
{
    public interface IGameRootObject
    {
        Transform CommonRootTransform { get; }
    }
}