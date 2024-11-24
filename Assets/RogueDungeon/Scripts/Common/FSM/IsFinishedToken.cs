namespace Common.FSM
{
    public class IsFinishedToken : IFinishable, IFinishableSetter
    {
        public bool IsFinished { get; set; }
    }
}