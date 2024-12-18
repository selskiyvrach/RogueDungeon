using Common.Animations;
using Common.Fsm;
using RogueDungeon.Fsm;
using RogueDungeon.Items;
using RogueDungeon.Player;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackPrepareState : BoundToAnimationState
    {
        private readonly IAttackIdleTransitionDuration _duration;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;

        protected override AnimationData Animation => new(AnimationNames.ATTACK_PREPARE_TO_BOTTOM_LEFT, _duration.Value);

        public AttackPrepareState(IAnimator animator, IAttackIdleTransitionDuration duration, IControlState controlState, IComboCounter comboCounter) : base(animator)
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