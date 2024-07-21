using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
{
    public class FinishableByAnimationState<T> : StateWithHandlers, IFinishable where T : IAnimation, IFinishable
    {
        private T _animation;
        public bool IsFinished => _animation.IsFinished;

        public FinishableByAnimationState(T animation) => 
            _animation = animation;
    }
}