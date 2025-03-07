using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Animations
{
    public class SpriteSheetAnimation : Animation
    {
        private readonly ISpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        private readonly Sprite[] _sprites;
        
        protected override float Duration { get; }
        protected override IEnumerable<(float time, string name)> Events { get; }

        public SpriteSheetAnimation(ISpriteSheetAnimationTarget spriteSheetAnimationTarget, Sprite[] sprites, float duration, IEnumerable<(int frame, string name)> events)
        {
            _spriteSheetAnimationTarget = spriteSheetAnimationTarget;
            _sprites = sprites;
            Duration = duration;
            Events = events.Select(n => (n.frame / (float)_sprites.Length * Duration, n.name)).ToArray();
        }

        protected override void ApplyAnimation(float timeNormalized) => 
            _spriteSheetAnimationTarget.SpriteRenderer.sprite = _sprites[Mathf.RoundToInt(_sprites.Length * timeNormalized)];
    }
}