using System.Linq;
using Common.Animations;
using DG.Tweening;
using UnityEngine;
using Animation = Common.Animations.Animation;
using AnimationEvent = Common.Animations.AnimationEvent;

namespace RogueDungeon.Items
{
    public class ItemAnimation : Animation
    {
        private readonly ItemAnimationConfig _config;
        private readonly Sequence _tweener;
        
        protected override AnimationEvent[] Events => _config.Events;
        public ItemAnimation(IAnimationClipTarget target, ItemAnimationConfig config) : base(config)
        {
            _config = config;
            
            _tweener = DOTween.Sequence();
            var keyframes = _config.KeyFrames.OrderBy(n => n.Time).ToArray();
            var peviousTime = 0f;
            foreach (var keyframe in keyframes)
            {
                var time = keyframe.Time - peviousTime;
                _tweener.Append(target.GameObject.transform.DOLocalMove(keyframe.Position, time));
                _tweener.Join(target.GameObject.transform.DOLocalRotate(keyframe.Rotation, time));
                peviousTime = keyframe.Time;
            }
        }

        protected override void ApplyAnimation(float timeNormalized) => 
            _tweener.Goto(_tweener.Duration() * timeNormalized, true);
    }
}