using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Animations
{
    [Serializable]
    public class AnimationConfigPicker
    {
        public enum AnimationType
        {
            AnimationClipAdapter,
            SpriteSheet,
        }
        
        [SerializeField] private AnimationType _animationType;
        [SerializeField, HideLabel, ShowIf("@_animationType == AnimationType.AnimationClipAdapter")] private AnimationClipAdapterConfig _animationClipConfig;
        [SerializeField, HideLabel, ShowIf("@_animationType == AnimationType.SpriteSheet")] private SpriteSheetAnimationConfig _spriteSheetAnimationConfig;

        public AnimationConfig Config => _animationType switch
        {
            AnimationType.AnimationClipAdapter => _animationClipConfig,
            AnimationType.SpriteSheet => _spriteSheetAnimationConfig,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}