using System.Collections.Generic;
using System.Linq;
using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.MoveSets
{
    public class Move : BoundToAnimationState, IIdBasedTransitionableState
    {
        public MoveConfig Config { get; }
        protected override AnimationData Animation => new(Config.AnimationClip.name, Config.Duration);
        public string Id => Config.Id;
        public List<Move> Transitions { get; } = new();

        protected Move(MoveConfig config, IAnimator animator) : base(animator) => 
            Config = config;

        public string GetTransitionStateId() =>
            !IsFinished 
                ? null 
                : Transitions.First(n => n.CanTransitionTo()).Id;

        protected virtual bool CanTransitionTo() => true;
    }
}