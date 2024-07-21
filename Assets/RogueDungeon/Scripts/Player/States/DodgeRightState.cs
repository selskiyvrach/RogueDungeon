namespace RogueDungeon.Player.States
{
    public class DodgeRightState : FinishableByAnimationState<IDodgeAnimation>
    {
        public DodgeRightState(IDodgeAnimation animation) : base(animation)
        {
        }
    }
}