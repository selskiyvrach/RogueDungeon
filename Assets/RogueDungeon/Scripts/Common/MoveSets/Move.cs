using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.MoveSets
{
    public class Move : IMove
    {
        private readonly Func<AnimationClip> _animationGetter;
        private readonly Func<float> _durationGetter;
        
        public List<IMove> TransitionsUnderlyingList { get; } = new();
        public string Name { get; }
        public AnimationClip Animation => _animationGetter.Invoke();
        public float Duration => _durationGetter.Invoke();
        public IEnumerable<IMove> Transitions => TransitionsUnderlyingList;

        public Move(string name, Func<AnimationClip> animationGetter, Func<float> durationGetter)
        {
            Name = name;
            _animationGetter = animationGetter;
            _durationGetter = durationGetter;
        }
    }
}