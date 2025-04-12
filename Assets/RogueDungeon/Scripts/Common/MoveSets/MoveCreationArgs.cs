using System;
using System.Collections.Generic;
using Common.Animations;

namespace Common.MoveSets
{
    public readonly struct MoveCreationArgs
    {
        public readonly string Id;
        public readonly Type MoveType;
        public readonly object MoveConstructorArgs;
        public readonly AnimationConfig AnimationConfig;
        public readonly IEnumerable<TransitionPicker> Transitions;

        public MoveCreationArgs(string id, Type moveType, object moveConstructorArgs, AnimationConfig animationConfig, IEnumerable<TransitionPicker> transitions)
        {
            Id = id;
            MoveType = moveType;
            MoveConstructorArgs = moveConstructorArgs;
            AnimationConfig = animationConfig;
            Transitions = transitions;
        }
    }
}