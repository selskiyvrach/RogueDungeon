using UnityEngine;

namespace Libs.Animations
{
    public class HandTransformAnimationTarget : TransformAnimationTarget
    {
        [field: SerializeField] public bool IsRightHand { get; private set; }
    }
}