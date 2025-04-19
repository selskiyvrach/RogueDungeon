using System.Linq;
using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;

namespace Common.MoveSets
{
    public abstract class Move : BoundToAnimationState, IIdBasedTransitionableState
    {
        protected override IAnimation Animation { get; }

        public string Id { get; }
        public Transition[] Transitions { get; set; }
        
        protected Move(string id, IAnimation animation)
        {
            Id = id;
            Animation = animation;
        }

        public virtual string GetTransitionStateId() => 
            Transitions.FirstOrDefault(n => (IsFinished || n.CanInterrupt) && n.Move.CanTransitionTo())?.Move.Id;

        protected virtual bool CanTransitionTo() => true;

        public override string ToString() => $"[Move: {Id}]";

        protected override void OnAnimationEvent(string name)
        {
        }
    }
}