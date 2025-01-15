using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Fsm;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    internal class AttackPrepareState : BoundToAnimationState
    {
        private readonly IParameter<IAttackIdleTransitionDuration> _duration;
        private readonly ICanAttackGetter _canAttackGetter;
        private readonly IComboCounter _comboCounter;

        protected override AnimationData Animation => new(AnimationNames.ATTACK_PREPARE_TO_BOTTOM_LEFT, _duration.Value);

        public AttackPrepareState(IAnimator animator, IParameter<IAttackIdleTransitionDuration> duration, ICanAttackGetter canAttackGetter, IComboCounter comboCounter) : base(animator)
        {
            _duration = duration;
            _canAttackGetter = canAttackGetter;
            _comboCounter = comboCounter;
        }

        public override void Enter()
        {
            _comboCounter.AttackIndex++;
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsFinished)
                return;
            if(_canAttackGetter.CanAttack)
                stateChanger.To<AttackExecutionState>();
            else
                stateChanger.To<AttackFinishState<RightDirection>>();
        }
    }
}