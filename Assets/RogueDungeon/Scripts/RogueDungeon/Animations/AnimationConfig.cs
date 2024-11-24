using System;
using UnityEngine;

namespace RogueDungeon.Animations
{
    [Serializable]
    public class AnimationConfig
    {
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private bool _loop;

        public AnimationClip AnimationClip => _animationClip;
        public float Duration => _duration;
        public bool Loop => _loop;
    }
}