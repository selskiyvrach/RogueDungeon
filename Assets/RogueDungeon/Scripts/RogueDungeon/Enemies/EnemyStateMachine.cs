using Common.Fsm;
using RogueDungeon.Enemies.States;
using Zenject;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Enemies
{
    public class EnemyStateMachine : ITickable
    {
        private EnemyState _idleState;
        public EnemyState CurrentState { get; private set; }

        public void Initialize(EnemyState idleState) => 
            ChangeState(_idleState = idleState);

        public bool TryStartState(EnemyState state)
        {
            if (state.Priority <= CurrentState.Priority)
                return false;
            
            ChangeState(state);
            return true;
        }

        public void Tick(float timeDelta)
        {
            CurrentState.Tick(timeDelta);
            if(CurrentState.IsFinished)
                ChangeState(_idleState);
        }

        private void ChangeState(EnemyState state)
        {
            (CurrentState as IExitableState)?.Exit();
            CurrentState = state;
            (CurrentState as IEnterableState)?.Enter();
        }
    }
}