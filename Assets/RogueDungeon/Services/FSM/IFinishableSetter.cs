namespace RogueDungeon.Services.FSM
{
    public interface IFinishableSetter
    {
        bool IsFinished { set; }
    }
}