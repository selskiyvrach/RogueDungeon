namespace RogueDungeon.StateMachine
{
    public class TransitionsContainer
    {
        private readonly Transitions _transitions = new();

        public void Add(ITransition transition) => 
            _transitions.Add(transition);

        public bool CanTransition(StatesContainer statesContainer, out IState newState)
        {
            newState = null;
            
            foreach (var transition in _transitions)
            {
                if(!transition.CanTransit(statesContainer, out var state))
                    continue;
                newState = state;
                break;
            }

            return newState != null;
        }
    }
}