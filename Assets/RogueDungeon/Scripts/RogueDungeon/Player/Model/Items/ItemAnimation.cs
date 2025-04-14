using System.Linq;
using Common.Animations;
using DG.Tweening;
using Animation = Common.Animations.Animation;
using AnimationEvent = Common.Animations.AnimationEvent;

namespace RogueDungeon.Items
{
    public class ItemAnimation : Animation
    {
        private readonly ItemAnimationConfig _config;
        private readonly IAnimationClipTarget _target;
        
        private Sequence _tweener;

        protected override AnimationEvent[] Events => _config.Events;
        public ItemAnimation(IAnimationClipTarget target, ItemAnimationConfig config) : base(config)
        {
            _config = config;
            _target = target;
        }

        public override void Play()
        {
            if(_config.KeyFrames.Length == 0) 
                return;
            
            _tweener?.Kill();
            _tweener = DOTween.Sequence();
            var keyframes = _config.KeyFrames.OrderBy(n => n.Time).ToArray();
            var peviousTime = 0f;
            foreach (var keyframe in keyframes)
            {
                var time = keyframe.Time - peviousTime;
                _tweener.Append(_target.GameObject.transform.DOLocalMove(keyframe.Position, time).SetEase(Ease.InOutQuad));
                _tweener.Join(_target.GameObject.transform.DOLocalRotate(keyframe.Rotation, time).SetEase(Ease.InOutQuad));
                peviousTime = keyframe.Time;
            }
            base.Play();
        }

        protected override void ApplyAnimation(float timeNormalized)
        {
            if(_tweener == null)
                return;
            
            var position = _tweener.Duration() * timeNormalized; 
            _tweener.Goto(position, true);
        }
    }
}