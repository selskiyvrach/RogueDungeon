using UnityEngine;

namespace RogueDungeon.Game
{
    public class GameRootObject : MonoBehaviour, IGameRootObject, IUiRootObject
    {
        [SerializeField] private Transform _uiTransform;
        public Transform UiRootTransform => _uiTransform;
        public Transform CommonRootTransform => transform;
    }
}