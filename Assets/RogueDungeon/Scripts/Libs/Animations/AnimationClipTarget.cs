using UnityEngine;

namespace Libs.Animations
{
    public class AnimationClipTarget : MonoBehaviour, IAnimationClipTarget
    {
        [field: SerializeField] public GameObject GameObject { get; private set; }
    }
}