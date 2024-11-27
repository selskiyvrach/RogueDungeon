namespace RogueDungeon.Player
{
    public interface IDodger
    {
        void StartDodge(DodgeEvent.DodgeDirection dodgeDirection);
        void FinishDodge();
    }
}