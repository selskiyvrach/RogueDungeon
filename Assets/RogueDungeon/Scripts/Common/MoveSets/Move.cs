using System.Linq;
using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;

namespace Common.MoveSets
{
    public class Move : BoundToAnimationState, IIdBasedTransitionableState
    {
        protected override IAnimation Animation { get; }
        
        public MoveConfig Config { get; }
        public string Id => Config.Id;
        public Move[] Transitions { get; set; }

        protected Move(MoveConfig config, IAnimation animation)
        {
            Config = config;
            Animation = animation;
        }

        public string GetTransitionStateId()
        {
            if (Id == "idle")
            {
                var transition = Transitions.FirstOrDefault(n => n.CanTransitionTo())?.Id;
                return IsFinished
                    ? transition ?? "idle"
                    : transition;
            }
            
            return IsFinished
                ? Transitions.FirstOrDefault(n => n.CanTransitionTo())?.Id
                : null;
        }

        protected virtual bool CanTransitionTo() => true;

        public override string ToString() => $"[Move: {Id}]";

        protected override void OnAnimationEvent(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}