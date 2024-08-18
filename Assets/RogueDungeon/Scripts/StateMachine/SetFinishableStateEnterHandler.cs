namespace RogueDungeon.StateMachine
{
    public class SetFinishableStateEnterHandler : IStateEnterHandler
    {
        private readonly IFinishableSetter _finishableSetter;
        private readonly bool _value;
 
        public SetFinishableStateEnterHandler(IFinishableSetter finishableSetter, bool value)
        {
            _finishableSetter = finishableSetter;
            _value = value;
        }

        public void OnEnter() => 
            _finishableSetter.IsFinished = _value;
    }
}