using System;
using UnityEngine;

namespace RogueDungeon.Animations
{
    [Serializable]
    public class AnimationConfig
    {
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private bool _loop;

        public AnimationClip AnimationClip => _animationClip;
        public bool Loop => _loop;
    }
}