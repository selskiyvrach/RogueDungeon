using System;
using Game.Features.Enemies.Domain.Moves;
using Libs.Lifecycle;

namespace Game.Features.Enemies.Domain
{
    public class EnemyStateMachine : ITickable
    {
        private EnemyMove _idleMove;
        public EnemyMove CurrentMove { get; private set; }
        public event Action<EnemyMove, EnemyMove> OnStateChanged;

        public void Initialize(EnemyMove idleMove) => 
            ChangeState(_idleMove = idleMove);

        public bool TryStartState(EnemyMove move)
        {
            if (move.Priority <= CurrentMove.Priority)
                return false;
            StartState(move);
            
            return true;
        }
        
        public void StartState(EnemyMove move) => 
            ChangeState(move);

        public void Tick(float timeDelta)
        {
            CurrentMove.Tick(timeDelta);
            if(CurrentMove.IsFinished)
                ChangeState(_idleMove);
        }

        private void ChangeState(EnemyMove move)
        {
            var previousState = CurrentMove;
            (CurrentMove as IExitableState)?.Exit();
            CurrentMove = move;
            (CurrentMove as IEnterableState)?.Enter();
            OnStateChanged?.Invoke(previousState, CurrentMove);
        }
    }
}