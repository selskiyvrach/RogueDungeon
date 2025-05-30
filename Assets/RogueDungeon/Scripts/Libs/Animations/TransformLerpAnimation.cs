using UnityEngine;
using UnityEngine.Assertions;

namespace Libs.Animations
{
    public class TransformLerpAnimation : Animation
    {
        private readonly AnimationCurve _curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        private readonly TransformAnimationConfig _config;
        private readonly TransformAnimationTarget _target;
        private readonly KeyFrame[] _keyframes;
        private readonly bool _hasNoStartingFrame;

        protected override AnimationEvent[] Events => _config.Events;
        
        public TransformLerpAnimation(TransformAnimationTarget target, TransformAnimationConfig config) : base(config)
        {
            _config = config;
            _target = target;
            if (_config.KeyFrames.Length == 0)
                return;
            
            _hasNoStartingFrame = _config.KeyFrames[0].Time > 0;
            _keyframes = new KeyFrame[_config.KeyFrames.Length > 0 ? _config.KeyFrames.Length + (_hasNoStartingFrame ? 1 : 0) : 0];

            Assert.IsTrue(_config.KeyFrames[^1].Time == 1, "No final keyframe found!");
        }

        public override void Play()
        {
            if (_config.KeyFrames.Length == 0)
            {
                base.Play();
                return;
            }
            
            if(_hasNoStartingFrame)
                _keyframes[0] = new KeyFrame(0, GetProperHandPosition(_target.LocalPosition), GetProperHandRotation(_target.LocalRotation));
            // not baked so I can tweak the values in runtime
            for (var i = _hasNoStartingFrame ? 1 : 0; i < _keyframes.Length; i++)
                _keyframes[i] = _config.KeyFrames[_hasNoStartingFrame ? i - 1 : i];
            
            base.Play();
        }

        protected override void ApplyAnimation(float timeNormalized)
        {
            if(_config.KeyFrames.Length == 0)
                return;

            var fromFrame = (KeyFrame?)null;
            var toFrame = (KeyFrame?)null;

            for (var i = 0; i < _keyframes.Length; i++)
            {
                var frame = _keyframes[i];
                if (frame.Time < timeNormalized)
                    continue;
                
                toFrame = frame;
                fromFrame = i == 0 ? toFrame : _keyframes[i - 1];
                break;
            }

            Assert.IsTrue(fromFrame is not null && toFrame is not null, "Failed to find proper keyframe(s)");

            var normTimeBetweenKeyframes = timeNormalized == 0 || toFrame.Value.Time - fromFrame.Value.Time == 0 
                ? 0 :
                timeNormalized == 1 
                    ? 1
                    : (timeNormalized - fromFrame.Value.Time) / (toFrame.Value.Time - fromFrame.Value.Time);
            
            normTimeBetweenKeyframes = _curve.Evaluate(normTimeBetweenKeyframes);
            _target.LocalPosition = Vector3.Lerp(GetProperHandPosition(fromFrame.Value.Position), GetProperHandPosition(toFrame.Value.Position), normTimeBetweenKeyframes);
            _target.LocalRotation = Vector3.Lerp(GetProperHandRotation(fromFrame.Value.Rotation), GetProperHandRotation(toFrame.Value.Rotation), normTimeBetweenKeyframes);
        }

        private Vector3 GetProperHandRotation(Vector3 rotation) => 
            _target is not HandTransformAnimationTarget hand || hand.IsRightHand ? rotation : new Vector3(-rotation.x, -rotation.y + 180, rotation.z);

        private Vector3 GetProperHandPosition(Vector3 pos) => 
            _target is not HandTransformAnimationTarget hand || hand.IsRightHand ? pos : new Vector3(-pos.x, pos.y, pos.z);
    }
}