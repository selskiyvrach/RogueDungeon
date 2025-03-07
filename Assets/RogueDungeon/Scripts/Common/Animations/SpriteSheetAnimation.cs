using System.Linq;
using UnityEngine;

namespace Common.Animations
{
    public class SpriteSheetAnimation : Animation
    {
        private readonly SpriteSheetAnimationConfig _config;
        private readonly ISpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        private readonly Sprite[] _sprites;
        
        protected override AnimationEvent[] Events { get; }

        public SpriteSheetAnimation(ISpriteSheetAnimationTarget spriteSheetAnimationTarget, SpriteSheetAnimationConfig config) : base(config)
        {
            _spriteSheetAnimationTarget = spriteSheetAnimationTarget;
            _config = config;
            Events = config.KeyFrames.Select(n => new AnimationEvent(n.Frame / (float)config.Sprites.Length * config.Duration, n.Name)).ToArray();
        }

        protected override void ApplyAnimation(float timeNormalized) => 
            _spriteSheetAnimationTarget.SpriteRenderer.sprite = _sprites[Mathf.RoundToInt(_sprites.Length * timeNormalized)];
    }
}