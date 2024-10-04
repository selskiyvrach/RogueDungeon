using RogueDungeon.StateMachine;
using ITickable = RogueDungeon.StateMachine.ITickable;

namespace RogueDungeon.Player.States
{
    public class EquipmentManipulationState : IFinishableState, IEnterable, IExitable, ITickable, ICondition, IItemManipulator
    {
        private readonly ICurrentItemManipulatorProvider _currentItemManipulatorProvider;
        public ICondition EnterCondition => this;
        public IFinishableState State => this;
        public bool IsFinished => _currentItemManipulatorProvider.CurrentManipulator.Value.State.IsFinished;
        
        public EquipmentManipulationState(ICurrentItemManipulatorProvider currentItemManipulatorProvider) => 
            _currentItemManipulatorProvider = currentItemManipulatorProvider;

        public void Enter() => 
            (_currentItemManipulatorProvider.CurrentManipulator.Value.State as IEnterable)?.Enter();

        public void Exit() => 
            (_currentItemManipulatorProvider.CurrentManipulator.Value.State as IExitable)?.Exit();

        public void Tick() => 
            (_currentItemManipulatorProvider.CurrentManipulator.Value.State as ITickable)?.Tick();

        bool ICondition.IsMet() => 
            _currentItemManipulatorProvider.CurrentManipulator.Value.EnterCondition.IsMet();
    }
}