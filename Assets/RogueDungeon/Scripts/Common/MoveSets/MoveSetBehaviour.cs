using Common.Behaviours;
using Common.Fsm;

namespace Common.MoveSets
{
    internal class MoveSetBehaviour : StateMachineBehaviour
    {
        private readonly IPendingMoveSetter _pendingMoveSetter;
        private readonly IMoveSetMovesGetter _movesGetter;
        
        public MoveSetBehaviour(IPendingMoveSetter pendingMoveSetter, IStatesFactory statesFactory, IMoveSetMovesGetter movesGetter, ILogger logger = null) : base(statesFactory, logger)
        {
            _pendingMoveSetter = pendingMoveSetter;
            _movesGetter = movesGetter;
        }

        protected override void ToStartState()
        {
            _pendingMoveSetter.PendingMove = _movesGetter.IdleMove;
            To<MoveExecutionState>();
        }
    }
}