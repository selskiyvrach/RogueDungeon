using RogueDungeon.Animations;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay
{
    public class PlayAnimationStateHandler : IStateEnterHandler, IStateExitHandler
    {
        private readonly IAnimation _animation;

        public PlayAnimationStateHandler(IAnimation animation) => 
            _animation = animation;

        public void OnEnter() => 
            _animation.Play();

        public void OnExit() => 
            _animation.Stop();
    }
}