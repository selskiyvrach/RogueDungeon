using System.Collections.Generic;
using System.Linq;

namespace Common.FSM
{
    public class StatesContainer
    {
        private IState _startState;
        private readonly HashSet<IState> _states = new();

        public void AddStartState(IState startState)
        {
            Add(startState);
            _startState = startState;
        }

        public void Add(IState state) => 
            _states.Add(state);
        
        public void Add(IEnumerable<IState> states)
        {
            foreach (var state in states) 
                Add(state);
        }

        public IState GetStartState() => 
            _startState;
        
        /// <summary>
        /// Use to make sure that said state belongs to this sets set
        /// </summary>
        public IState Get(IState requiredState) =>
            _states.FirstOrDefault(n => n == requiredState);
    }
}