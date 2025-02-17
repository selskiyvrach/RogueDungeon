using System.Linq;
using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;

namespace Common.MoveSets
{
    public class Move : BoundToAnimationState, IIdBasedTransitionableState
    {
        public MoveConfig Config { get; }
        protected override AnimationData Animation => new(Config.AnimationClip.name, Config.Duration);
        public string Id => Config.Id;
        public Move[] Transitions { get; set; }

        protected Move(MoveConfig config, IAnimator animator) : base(animator) => 
            Config = config;

        public string GetTransitionStateId()
        {
            if (Id == "Idle")
            {
                var transition = Transitions.FirstOrDefault(n => n.CanTransitionTo())?.Id;
                return IsFinished
                    ? transition ?? "Idle"
                    : transition;
            }
            
            return IsFinished
                ? Transitions.First(n => n.CanTransitionTo()).Id
                : null;
        }

        protected virtual bool CanTransitionTo() => true;
        public override string ToString() => $"[Move: {Id}]";
    }
}