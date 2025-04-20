using System;
using Zenject;

namespace Common.Animations
{
    [Serializable]
    public abstract class AnimationConfig
    {
        public abstract IAnimation Create(DiContainer container);
    }
}