using Common.Animations;
using Common.Fsm;
using RogueDungeon.Player;
using RogueDungeon.Player.Input;

namespace RogueDungeon.Items.Handling.WeaponWielder
{
    internal class IdleState : IState, IEnterableState
    {
        private readonly IAnimator _animator;
        private readonly IInput _input;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;
        private readonly IIdleAnimationSpeed _animationSpeed;

        public IdleState(IAnimator animator, IInput input, IControlState controlState, IComboCounter comboCounter, IIdleAnimationSpeed animationSpeed)
        {
            _animator = animator;
            _input = input;
            _controlState = controlState;
            _comboCounter = comboCounter;
            _animationSpeed = animationSpeed;
        }

        public void Enter()
        {
            _comboCounter.AttackIndex = -1;
            _animator.Play(new LoopedAnimationData(AnimationNames.IDLE, _animationSpeed.Value));
        }

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if(_controlState.CanAttack() && _input.TryConsume(Input.Attack))
                stateChanger.To<AttackPrepareState>();
        }
    }
}