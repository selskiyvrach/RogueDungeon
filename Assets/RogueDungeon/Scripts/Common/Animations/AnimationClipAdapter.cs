using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
using UnityEngine;

namespace Common.Animations
{
    public class AnimationClipAdapter : Animation
    {
        private readonly AnimationClip _clip;
        private readonly GameObject _target;

        protected override float Duration { get; }
        protected override IEnumerable<(float time, string name)> Events { get; }

        public AnimationClipAdapter(AnimationClip clip, float duration, GameObject target)
        {
            _clip = clip;
            _target = target;
            Duration = duration;
            Events = _clip.events.Where(n => !n.stringParameter.IsNullOrEmpty()).Select(n => (n.time, n.stringParameter)).ToArray();
        }

        protected override void ApplyAnimation(float timeNormalized) => 
            _clip.SampleAnimation(_target, _clip.length * timeNormalized);
    }
}