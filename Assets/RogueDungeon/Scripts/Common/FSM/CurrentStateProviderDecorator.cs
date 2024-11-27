namespace Common.FSM
{
    public class CurrentStateProviderDecorator : ICurrentStateProvider
    {
        public ICurrentStateProvider Provider { get; set; }
        public IState CurrentState => Provider?.CurrentState;
    }
}