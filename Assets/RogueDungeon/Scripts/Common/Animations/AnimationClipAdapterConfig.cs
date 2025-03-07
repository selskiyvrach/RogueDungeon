using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Animations
{
    [Serializable]
    public class AnimationClipAdapterConfig : AnimationConfigWithDuration
    {
        [field: SerializeField] public AnimationClip Clip { get; private set; }
        public override Type AnimationType => typeof(AnimationClipAdapter);
    }
}