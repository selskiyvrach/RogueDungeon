using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.Animations
{
    [Serializable]
    public class AnimationConfigPicker
    {
        public enum AnimationType
        {
            AnimationClipAdapter,
            SpriteSheet,
            TransformLerp,
        }
        
        [SerializeField] private AnimationType _animationType;
        [SerializeField, HideLabel, ShowIf("@_animationType == AnimationType.AnimationClipAdapter")] private AnimationClipAdapterConfig _animationClipConfig;
        [SerializeField, HideLabel, ShowIf("@_animationType == AnimationType.SpriteSheet")] private SpriteSheetAnimationConfig _spriteSheetAnimationConfig;
        [SerializeField, HideLabel, ShowIf("@_animationType == AnimationType.TransformLerp")] private TransformAnimationConfig _transformAnimationConfig;

        public AnimationConfig Config => _animationType switch
        {
            AnimationType.AnimationClipAdapter => _animationClipConfig,
            AnimationType.SpriteSheet => _spriteSheetAnimationConfig,
            AnimationType.TransformLerp => _transformAnimationConfig,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}