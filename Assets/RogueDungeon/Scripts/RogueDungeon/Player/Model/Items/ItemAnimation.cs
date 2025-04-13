using DG.Tweening;
using UnityEngine;
using Animation = Common.Animations.Animation;
using AnimationEvent = Common.Animations.AnimationEvent;

namespace RogueDungeon.Items
{
    public class ItemAnimation : Animation
    {
        private readonly Transform _target;
        private readonly ItemAnimationConfig _config;
        private Sequence _tweener;
        
        protected override AnimationEvent[] Events => _config.Events;
        public ItemAnimation(Transform target, ItemAnimationConfig config) : base(config)
        {
            _target = target;
            _config = config;
        }

        public override void Play()
        {
            base.Play();
            _tweener?.Kill(true);
            _tweener = DOTween.Sequence();
            _tweener.Append(_target.DOLocalMove(_config.EndPosition, 1));
            _tweener.Join(_target.DOLocalRotate(_config.EndRotation, 1));
        }

        protected override void ApplyAnimation(float timeNormalized) => 
            _tweener.Goto(_tweener.Duration() * timeNormalized, true);
    }
}