namespace RogueDungeon.StateMachine
{
    public class IsFinishedToken : IFinishable, IFinishableSetter
    {
        public bool IsFinished { get; set; }
    }
}