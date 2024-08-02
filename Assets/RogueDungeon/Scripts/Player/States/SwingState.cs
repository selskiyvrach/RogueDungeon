namespace RogueDungeon.Player.States
{
    public class SwingState : FinishableByAnimationState<ISwingAnimation>
    {
        public SwingState(ISwingAnimation animation) : base(animation)
        {
        }
    }
}