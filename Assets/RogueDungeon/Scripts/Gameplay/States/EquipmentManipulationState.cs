using RogueDungeon.Services.FSM;
using FSM_ITickable = RogueDungeon.Services.FSM.ITickable;
using ITickable = RogueDungeon.Services.FSM.ITickable;

namespace RogueDungeon.Gameplay.States
{
    public class EquipmentManipulationState : IFinishableState, IEnterable, IExitable, FSM_ITickable, ICondition, IItemManipulationStateInfo
    {
        private readonly IItemManipulatorProvider _itemManipulatorProvider;
        public ICondition EnterCondition => this;
        public IFinishableState State => this;
        public bool IsFinished => _itemManipulatorProvider.CurrentItemManipulator.Value.State.IsFinished;
        
        public EquipmentManipulationState(IItemManipulatorProvider itemManipulatorProvider) => 
            _itemManipulatorProvider = itemManipulatorProvider;

        public void Enter() => 
            (_itemManipulatorProvider.CurrentItemManipulator.Value.State as IEnterable)?.Enter();

        public void Exit() => 
            (_itemManipulatorProvider.CurrentItemManipulator.Value.State as IExitable)?.Exit();

        public void Tick() => 
            (_itemManipulatorProvider.CurrentItemManipulator.Value.State as ITickable)?.Tick();

        bool ICondition.IsMet() =>
            _itemManipulatorProvider.CurrentItemManipulator?.Value?.EnterCondition.IsMet() ?? false;
    }
}