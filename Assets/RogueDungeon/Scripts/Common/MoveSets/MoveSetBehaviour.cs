using Common.Behaviours;
using Common.Fsm;

namespace Common.MoveSets
{
    internal class MoveSetBehaviour : StateMachineBehaviour
    {
        public MoveSetBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<MoveSetIdleState>();
    }
}