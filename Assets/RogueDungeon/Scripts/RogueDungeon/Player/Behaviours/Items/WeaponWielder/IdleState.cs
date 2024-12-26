using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Characters.Commands;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    internal class IdleState : IState, IEnterableState
    {
        private readonly IAnimator _animator;
        private readonly ICharacterCommands _weaponInput;
        private readonly IWeaponControlState _controlState;
        private readonly IComboCounter _comboCounter;
        private readonly IParameter<IIdleAnimationSpeed> _animationSpeed;

        public IdleState(IAnimator animator, ICharacterCommands weaponInput, IWeaponControlState controlState, IComboCounter comboCounter, IParameter<IIdleAnimationSpeed> animationSpeed)
        {
            _animator = animator;
            _weaponInput = weaponInput;
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
            if(_controlState.CanAttack() && _weaponInput.TryConsume<IAttackCommand>())
                stateChanger.To<AttackPrepareState>();
        }
    }
}