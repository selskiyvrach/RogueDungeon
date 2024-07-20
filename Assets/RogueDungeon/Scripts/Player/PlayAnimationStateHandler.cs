using RogueDungeon.StateMachine;

namespace RogueDungeon.Player
{
    public class PlayAnimationStateHandler<TAnimation> : IStateEnterHandler, IStateExitHandler where TAnimation : IAnimation
    {
        private TAnimation _animation;

        public PlayAnimationStateHandler(TAnimation animation) =>
            _animation = animation;

        public void OnEnter() => 
            _animation.Play();

        public void OnExit() => 
            _animation.Stop();
    }
}