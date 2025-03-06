using System.Collections.Generic;
using Common.Fsm;

namespace Common.MoveSets
{
    public class MoveSetBehaviour : Behaviours.StateMachineBehaviour
    {
        public IdBasedTransitionStrategy TransitionStrategy { get; }

        public MoveSetBehaviour(IEnumerable<Move> moves, string startStateId) : base(new StateMachine(new IdBasedTransitionStrategy(moves, startStateId))) => 
            TransitionStrategy = (IdBasedTransitionStrategy)StateMachine.TransitionStrategy;
    }
}