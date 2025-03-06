using System;
using System.Collections.Generic;
using System.Linq;
using Common.MoveSets;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyMoveSetBehaviour : MoveSetBehaviour
    {
        private EnemyMove _pendingMove;
        public IEnumerable<EnemyMove> Moves => TransitionStrategy.Moves.Values.Cast<EnemyMove>();

        public EnemyMove PendingMove
        {
            get => _pendingMove;
            set
            {
                Assert.IsTrue(value == null || Moves.Contains(value));
                _pendingMove = value;
                TryStartPendingMove();
            }
        }
        public EnemyMove CurrentMove => (EnemyMove)StateMachine.CurrentState;
        public bool IsIdle => CurrentMove.Id == "idle";

        public EnemyMoveSetBehaviour(IEnumerable<EnemyMove> moves, string startStateId) : base(moves, startStateId)
        {
        }

        private void TryStartPendingMove()
        {
            throw new NotImplementedException();
        }
    }
}