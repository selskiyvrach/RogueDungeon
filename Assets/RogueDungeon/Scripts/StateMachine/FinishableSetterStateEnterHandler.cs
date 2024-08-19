namespace RogueDungeon.StateMachine
{
    public class FinishableSetterStateEnterHandler : IStateEnterHandler
    {
        private readonly IFinishableSetter _finishableSetter;
        private readonly bool _value;
 
        public FinishableSetterStateEnterHandler(IFinishableSetter finishableSetter, bool value)
        {
            _finishableSetter = finishableSetter;
            _value = value;
        }

        public void OnEnter() => 
            _finishableSetter.IsFinished = _value;
    }
    
    public class FinishableSetterStateExitHandler : IStateExitHandler
    {
        private readonly IFinishableSetter _finishableSetter;
        private readonly bool _value;
 
        public FinishableSetterStateExitHandler(IFinishableSetter finishableSetter, bool value)
        {
            _finishableSetter = finishableSetter;
            _value = value;
        }

        public void OnExit() => 
            _finishableSetter.IsFinished = _value;
    }
}