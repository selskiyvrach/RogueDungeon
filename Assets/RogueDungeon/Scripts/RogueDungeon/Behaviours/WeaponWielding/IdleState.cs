using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class IdleState : State
    {
        private readonly IAnimator _animator;
        private readonly IDurations _durations;
        private readonly IInput _input;
        private readonly IControlState _controlState;
        
        public override void Enter() => 
            _animator.Play(new LoopedAnimationData(AnimationNames.IDLE, _durations.Get(Duration.Idle)));

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(_controlState.Is(AbleTo.StartAttack) && _input.TryConsume(Input.Attack))
                stateChanger.To<AttackPrepareState>();
        }
    }
}