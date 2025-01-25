using Common.Behaviours;

namespace Common.MoveSets
{
    internal class MoveSetInternalFacade : IBehaviourInternalFacade, ICurrentMoveGetter, ICurrentMoveSetter, IPendingMoveGetter, IPendingMoveSetter
    {
        private IMove _currentMove;
        private IMove _pendingMove;
        
        IMove ICurrentMoveGetter.CurrentMove => _currentMove;
        IMove ICurrentMoveSetter.CurrentMove
        {
            set => _currentMove = value;
        }

        IMove IPendingMoveGetter.PendingMove => _pendingMove;
        IMove IPendingMoveSetter.PendingMove
        {
            set => _pendingMove = value;
        }
    }
}