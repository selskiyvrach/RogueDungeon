using Common.UiCommons;
using UnityEngine;

namespace Common.UnityUtils
{
    public class GameRootObject : MonoBehaviour, ICommonRootObject, IUiRootObject
    {
        [SerializeField] private Transform _uiTransform;
        public Transform UiRootTransform => _uiTransform;
        public Transform CommonRootTransform => transform;
    }
}