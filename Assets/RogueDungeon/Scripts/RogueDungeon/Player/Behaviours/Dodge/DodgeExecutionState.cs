using System;
using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;
using Common.Parameters;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public abstract class DodgeExecutionState : BoundToAnimationState, ITypeBasedTransitionableState
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

        public void CheckTransitions(ITypeBasedStateChanger typeBasedStateChanger)
        {
            if(!IsFinished)
                return;
            _stateSetter.DodgeState = DodgeState.None;
            typeBasedStateChanger.ChangeState<DodgeIdleState>();
        }
    }
}