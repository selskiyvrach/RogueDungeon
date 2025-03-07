using UnityEngine;

namespace Common.Animations
{
    public class AnimationClipTarget : MonoBehaviour, IAnimationClipTarget
    {
        [field: SerializeField] public GameObject GameObject { get; private set; }
    }
}