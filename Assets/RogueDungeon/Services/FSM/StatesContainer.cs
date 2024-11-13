using System.Collections.Generic;
using System.Linq;

namespace RogueDungeon.Services.FSM
{
    public class StatesContainer
    {
        private IState _startState;
        private readonly HashSet<IState> _states = new();

        public void SetStartState(IState startState) => 
            _startState = startState;

        public void AddState(IState state) => 
            _states.Add(state);

        public IState GetStartState() => 
            _startState;
        
        /// <summary>
        /// Use to make sure that said state belongs to this sets set
        /// </summary>
        public IState GetState(IState requiredState) =>
            _states.FirstOrDefault(n => n == requiredState);
    }
}