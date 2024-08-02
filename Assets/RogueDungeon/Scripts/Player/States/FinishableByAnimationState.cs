using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
{
    public class FinishableByAnimationState<T> : FinishableByOtherState<T> where T : IAnimation, IFinishable
    {
        public FinishableByAnimationState(T animation, bool controlAnimation = true) : base(animation)
        {
            if (controlAnimation) 
                AddAllHandlerInterfaces(new PlayAnimationStateHandler<T>(animation));
        }
    }
}