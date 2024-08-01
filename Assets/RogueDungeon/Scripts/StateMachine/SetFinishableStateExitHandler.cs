namespace RogueDungeon.StateMachine
{
    public class SetFinishableStateExitHandler : IStateExitHandler
    {
        private readonly IFinishableSetter _finishableSetter;
        private readonly bool _value;
 
        public SetFinishableStateExitHandler(IFinishableSetter finishableSetter, bool value = true)
        {
            _finishableSetter = finishableSetter;
            _value = value;
        }

        public void OnExit() => 
            _finishableSetter.IsFinished = _value;
    }
}