using UnityEngine;

namespace Common.Animations
{
    public readonly struct AnimationData
    {
        public readonly AnimationClip Clip;
        public readonly float Duration;

        public AnimationData(AnimationClip clip, float duration)
        {
            Clip = clip;
            Duration = duration;
        }
    }
}