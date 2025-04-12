using System.Linq;
using Common.UtilsDotNet;

namespace Common.Animations
{
    public class AnimationClipAdapter : Animation
    {
        private readonly AnimationClipAdapterConfig _config;
        private readonly IAnimationClipTarget _target;

        protected override AnimationEvent[] Events { get; }

        public AnimationClipAdapter(IAnimationClipTarget target, AnimationClipAdapterConfig config) : base(config)
        {
            _target = target;
            _config = config;
            Events = _config.Clip.events.Where(n => !n.stringParameter.IsNullOrEmpty()).Select(n => new AnimationEvent(n.time / _config.Clip.length, n.stringParameter)).ToArray();
        }

        protected override void ApplyAnimation(float timeNormalized) => 
            _config.Clip.SampleAnimation(_target.GameObject, _config.Clip.length * timeNormalized);
    }
}