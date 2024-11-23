using UnityEngine;

namespace RogueDungeon.Game
{
    public class GameRootObject : MonoBehaviour, IGameRootObject, IUiRootObject
    {
        public Transform UiRootTransform => transform;
    }

    public interface IUiRootObject
    {
        Transform UiRootTransform { get; }
    }
}