using System;
using System.Collections.Generic;
using Common.Animations;

namespace Common.MoveSets
{
    public readonly struct MoveCreationArgs
    {
        public readonly string Id;
        public readonly Type MoveType;
        public readonly AnimationConfig AnimationConfig;
        public readonly IEnumerable<TransitionPicker> Transitions;
        public readonly object[] ExtraArguments;

        public MoveCreationArgs(string id, Type moveType, AnimationConfig animationConfig, IEnumerable<TransitionPicker> transitions, params object[] extraArguments)
        {
            Id = id;
            MoveType = moveType;
            AnimationConfig = animationConfig;
            Transitions = transitions;
            ExtraArguments = extraArguments;
        }
    }
}