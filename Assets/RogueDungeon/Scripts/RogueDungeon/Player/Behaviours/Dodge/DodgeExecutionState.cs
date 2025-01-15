using System;
using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Fsm;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public abstract class DodgeExecutionState : BoundToAnimationState
    {
        private readonly IParameter<IDodgeDuration> _duration;
        private readonly IDodgeStateSetter _stateSetter;

        protected abstract DodgeState DodgeState { get; }

        protected override AnimationData Animation => new(DodgeState switch {
            DodgeState.DodgingLeft => DodgeAnimationNames.DODGE_LEFT,
            DodgeState.DodgingRight => DodgeAnimationNames.DODGE_RIGHT,
            _ => throw new ArgumentOutOfRangeException()
        }, _duration.Value);

        protected DodgeExecutionState(IAnimator animator, IParameter<IDodgeDuration> duration, IDodgeStateSetter stateSetter) : base(animator)
        {
            _duration = duration;
            _stateSetter = stateSetter;
        }

        public override void Enter()
        {
            base.Enter();
            _stateSetter.DodgeState = DodgeState;
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsFinished)
                return;
            _stateSetter.DodgeState = DodgeState.None;
            stateChanger.To<DodgeIdleState>();
        }
    }
}