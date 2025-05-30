using System;
using Zenject;

namespace Libs.Animations
{
    [Serializable]
    public abstract class AnimationConfig
    {
        public abstract IAnimation Create(DiContainer container);
    }
}