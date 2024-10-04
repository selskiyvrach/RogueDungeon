using System;
using RogueDungeon.Player.States;
using UniRx;

namespace RogueDungeon.Player
{
    public class Player : IDieable, ICurrentItemManipulatorProvider
    {
        private readonly StateMachine.StateMachine _stateMachine;
        private IDisposable _tickSub;

        public IReadOnlyReactiveProperty<IItemManipulator> CurrentManipulator { get; } = new ReactiveProperty<IItemManipulator>();
        public bool IsDead { get; }

        public Player(StateMachine.StateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void SetTestItemManipulator(IItemManipulator manipulator) => 
            (CurrentManipulator as ReactiveProperty<IItemManipulator>).Value = manipulator;

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