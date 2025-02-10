using System.Linq;
using Common.Fsm;

namespace Common.MoveSets
{
    internal class MoveSetIdleState : IState
    {
        private readonly IMoveSetMovesGetter _moveSetMovesGetter;
        private readonly IPendingMoveSetter _pendingMoveSetter;
        private readonly ITryTransitionToMoveGetter _tryTransitionToMoveGetter;
        
        public void CheckTransitions(ITypeBasedStateChanger typeBasedStateChanger)
        {
            var moveToTransitionTo = _moveSetMovesGetter.All.FirstOrDefault(n => _tryTransitionToMoveGetter.TryTransitionTo(n));
            if(moveToTransitionTo is null)
                return;
            
            _pendingMoveSetter.PendingMove = moveToTransitionTo;
            typeBasedStateChanger.ChangeState<MoveExecutionState>();
        }
    }
}