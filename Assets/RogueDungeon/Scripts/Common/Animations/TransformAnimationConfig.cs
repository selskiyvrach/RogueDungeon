using System;
using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Common.Animations
{
    [Serializable]
    public class TransformAnimationConfig : AnimationConfig
    {
        [field: SerializeField] public KeyFrame[] KeyFrames { get; private set; }
        [field: SerializeField] public AnimationEvent[] Events { get; private set; }
        public override IAnimation Create(DiContainer container) => 
            (TransformLerpAnimation)container.Instantiate(typeof(TransformLerpAnimation), new object[]{this});
    }

    public class MultiItemTransformAnimationConfig : AnimationConfig
    {
        private readonly (string id, AnimationConfig config)[] _animations;

        public MultiItemTransformAnimationConfig(params (string id, AnimationConfig config)[] animations) : this(animations.Select(n => n)) { }
        
        public MultiItemTransformAnimationConfig(IEnumerable<(string id, AnimationConfig config)> animations)
        {
            _animations = animations.ToArray();
            Assert.IsTrue(_animations.Length > 0);
        }

        public override IAnimation Create(DiContainer container) =>
            new CompositeAnimation(_animations.Select(n => container.Instantiate(typeof(TransformLerpAnimation), new object[]
            {
                n.config, 
                n.id.IsNullOrEmpty() 
                    ? container.Resolve<TransformAnimationTarget>() 
                    : container.ResolveId<TransformAnimationTarget>(n.id),
            })).Cast<IAnimation>());
    }
}