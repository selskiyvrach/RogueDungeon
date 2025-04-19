using UnityEngine;
using UnityEngine.Assertions;
using Animation = Common.Animations.Animation;
using AnimationEvent = Common.Animations.AnimationEvent;

namespace RogueDungeon.Items
{
    public class ItemAnimation : Animation
    {
        private readonly ItemAnimationConfig _config;
        private readonly ItemAnimationClipTarget _target;
        private readonly KeyFrame[] _keyframes;
        private readonly bool _hasNoStartingFrame;
        
        private Vector3 _fromRotation;
        private Vector3 _toRotation;
        private Vector3 _fromPosition;
        private Vector3 _toPosition;
        private float _prevTimeNormalized;
        private int _frameIndex;

        protected override AnimationEvent[] Events => _config.Events;
        
        public ItemAnimation(ItemAnimationClipTarget target, ItemAnimationConfig config) : base(config)
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
            
            _prevTimeNormalized = 0;
            base.Play();
        }

        protected override void ApplyAnimation(float timeNormalized)
        {
            if(_config.KeyFrames.Length == 0)
                return;
            
            float? normTimeBetweenKeyframes = null;
            for (var i = 0; i < _keyframes.Length; i++)
            {
                if (normTimeBetweenKeyframes == null && timeNormalized >= _keyframes[i].Time) 
                    normTimeBetweenKeyframes = timeNormalized < 1 
                        ? (timeNormalized - _keyframes[i].Time) / (_keyframes[i + 1].Time - _keyframes[i].Time) 
                        : 1;

                // find just passed keyframe
                if (_prevTimeNormalized < _keyframes[i].Time && timeNormalized >= _keyframes[i].Time) 
                    continue;

                // grab its values as start values
                _fromRotation = _keyframes[i].Rotation;
                _fromPosition = _keyframes[i].Position;

                // if it's not the last one - grab target values
                if (i < _keyframes.Length - 1)
                {
                    _toRotation = _keyframes[i + 1].Rotation;
                    _toPosition = _keyframes[i + 1].Position;
                }
                break;
            }

            _target.LocalPosition = Vector3.Lerp(GetProperHandPosition(_fromPosition), GetProperHandPosition(_toPosition), (float)normTimeBetweenKeyframes);
            _target.LocalRotation = Vector3.Lerp(GetProperHandRotation(_fromRotation), GetProperHandRotation(_toRotation), (float)normTimeBetweenKeyframes);
            _prevTimeNormalized = timeNormalized;
        }

        private Vector3 GetProperHandRotation(Vector3 rotation) => 
            _target.IsRightHand ? rotation : new Vector3(-rotation.x, -rotation.y + 180, rotation.z);

        private Vector3 GetProperHandPosition(Vector3 pos) => 
            _target.IsRightHand ? pos : new Vector3(-pos.x, pos.y, pos.z);
    }
}