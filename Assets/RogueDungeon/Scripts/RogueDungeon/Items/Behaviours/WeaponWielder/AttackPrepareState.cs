using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Fsm;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    internal class AttackPrepareState : BoundToAnimationState
    {
        private readonly IParameter<IAttackIdleTransitionDuration> _duration;
        private readonly IWeaponControlState _controlState;
        private readonly IComboCounter _comboCounter;

        protected override AnimationData Animation => new(AnimationNames.ATTACK_PREPARE_TO_BOTTOM_LEFT, _duration.Value);

        public AttackPrepareState(IAnimator animator, IParameter<IAttackIdleTransitionDuration> duration, IWeaponControlState controlState, IComboCounter comboCounter) : base(animator)
        {
            _duration = duration;
            _controlState = controlState;
            _comboCounter = comboCounter;
        }

        public override void Enter()
        {
            _comboCounter.AttackIndex++;
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsTimerOff)
                return;
            if(_controlState.CanAttack())
                stateChanger.To<AttackExecutionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}