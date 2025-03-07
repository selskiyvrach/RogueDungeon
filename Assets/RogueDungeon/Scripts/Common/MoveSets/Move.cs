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
        public Transition[] Transitions { get; set; }

        protected Move(MoveConfig config, IAnimation animation)
        {
            Config = config;
            Animation = animation;
        }

        public string GetTransitionStateId() => 
            Transitions.FirstOrDefault(n => (IsFinished || n.CanInterrupt) && n.Move.CanTransitionTo())?.Move.Id;

        protected virtual bool CanTransitionTo() => true;

        public override string ToString() => $"[Move: {Id}]";

        protected override void OnAnimationEvent(string name)
        {
        }
    }
}