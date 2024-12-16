using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Characters;
using RogueDungeon.Parameters;
using RogueDungeon.PlayerInput;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class IdleState : IState, IEnterableState
    {
        private readonly IAnimator _animator;
        private readonly IParameters _durations;
        private readonly IInput _input;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;

        public IdleState(IAnimator animator, IParameters durations, IInput input, IControlState controlState, IComboCounter comboCounter)
        {
            _animator = animator;
            _durations = durations;
            _input = input;
            _controlState = controlState;
            _comboCounter = comboCounter;
        }

        public void Enter()
        {
            _comboCounter.AttackIndex = -1;
            _animator.Play(new LoopedAnimationData(AnimationNames.IDLE, _durations.Get(ParameterKeys.IDLE_ANIMATION_SPEED)));
        }

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if(_controlState.CanAttack() && _input.TryConsume(Input.Attack))
                stateChanger.To<AttackPrepareState>();
        }
    }
}