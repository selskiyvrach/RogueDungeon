using System;
using System.Linq;
using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;

namespace Common.MoveSets
{
    internal class MoveExecutionState : BoundToAnimationState
    {
        private readonly IPendingMoveGetter _pendingMoveGetter;
        private readonly IPendingMoveSetter _pendingMoveSetter;
        private readonly ICurrentMoveGetter _currentMoveGetter;
        private readonly ICurrentMoveSetter _currentMoveSetter;
        private readonly ITryTransitionToMoveGetter _tryTransitionToMoveGetter;

        protected override AnimationData Animation => new (_currentMoveGetter.CurrentMove.Animation.name, _currentMoveGetter.CurrentMove.Duration);
        
        public MoveExecutionState(IAnimator animator, IPendingMoveGetter pendingMoveGetter, IPendingMoveSetter pendingMoveSetter, ICurrentMoveSetter currentMoveSetter, ICurrentMoveGetter currentMoveGetter, ITryTransitionToMoveGetter tryTransitionToMoveGetter) : base(animator)
        {
            _pendingMoveGetter = pendingMoveGetter;
            _pendingMoveSetter = pendingMoveSetter;
            _currentMoveSetter = currentMoveSetter;
            _currentMoveGetter = currentMoveGetter;
            _tryTransitionToMoveGetter = tryTransitionToMoveGetter;
        }

        public override void Enter()
        {
            _currentMoveSetter.CurrentMove = _pendingMoveGetter.PendingMove;
            _pendingMoveSetter.PendingMove = null;
            base.Enter();
        }

        public override void CheckTransitions(ITypeBasedStateChanger typeBasedStateChanger)
        {
            if(!IsFinished)
                return;

            var transitionTo = _currentMoveGetter.CurrentMove.Transitions.FirstOrDefault(n => _tryTransitionToMoveGetter.TryTransitionTo(n));
            _pendingMoveSetter.PendingMove = transitionTo  ?? throw new Exception($"No transition from {_currentMoveGetter.CurrentMove.Name} found");
            typeBasedStateChanger.ChangeState<MoveExecutionState>();
        }
    }
}