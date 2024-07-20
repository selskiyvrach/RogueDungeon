using System;
using UniRx;

namespace RogueDungeon.Player
{
    public class Player : IDieable
    {
        private readonly StateMachine.StateMachine _stateMachine;
        private IDisposable _tickSub;

        public bool IsDead { get; }

        public Player(StateMachine.StateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Run()
        {
            _tickSub = Observable.EveryUpdate().Subscribe(_ => Tick());
            _stateMachine.Run();
        }

        public void Stop() => 
            _tickSub?.Dispose();

        private void Tick() => 
            _stateMachine.Tick();
    }
}