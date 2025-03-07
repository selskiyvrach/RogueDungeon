using System;
using UnityEngine;

namespace Common.Animations
{
    [Serializable]
    public abstract class AnimationConfigWithDuration : AnimationConfig
    {
        [field: SerializeField] public float Duration { get; private set; }
    }
}