using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
{
    public class FinishableByAnimationState<T> : FinishableByOtherState<T> where T : IAnimation, IFinishable
    {
        public FinishableByAnimationState(T animation) : base(animation)
        {
            
        }
    }
}