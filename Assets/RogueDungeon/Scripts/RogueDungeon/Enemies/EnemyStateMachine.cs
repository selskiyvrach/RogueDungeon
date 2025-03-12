using Common.Fsm;
using RogueDungeon.Enemies.MoveSet;
using Zenject;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Enemies
{
    public class EnemyStateMachine : ITickable, IInitializable
    {
        private readonly IFactory<EnemyStateConfig, EnemyState> _statesFactory;
        private EnemyState _idleState;
        private readonly EnemyIdleConfig _idleConfig;
        public EnemyState CurrentState { get; private set; }

        public EnemyStateMachine(EnemyIdleConfig idleState, IFactory<EnemyStateConfig, EnemyState> statesFactory)
        {
            _statesFactory = statesFactory;
            _idleConfig = idleState;
        }

        public void Initialize() => 
            ChangeState(_idleState = _statesFactory.Create(_idleConfig));

        public bool TryStartState(EnemyStateConfig config)
        {
            if (config.Priority <= CurrentState.Priority)
                return false;
            
            ChangeState(_statesFactory.Create(config));
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