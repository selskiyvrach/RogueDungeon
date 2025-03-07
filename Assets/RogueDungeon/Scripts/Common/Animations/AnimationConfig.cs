using System;
using System.Collections.Generic;

namespace Common.Animations
{
    [Serializable]
    public abstract class AnimationConfig
    {
        public abstract Type AnimationType { get; }
    }
}