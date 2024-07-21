namespace RogueDungeon.Player.States
{
    public class DodgeLeftState : FinishableByAnimationState<IDodgeAnimation>
    {
        public DodgeLeftState(IDodgeAnimation animation) : base(animation)
        {
        }
    }
}