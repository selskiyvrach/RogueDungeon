using RogueDungeon.StateMachine;
using ITickable = RogueDungeon.StateMachine.ITickable;

namespace RogueDungeon.Player.States
{
    public class EquipmentManipulationState : IFinishableState, IEnterable, IExitable, ITickable
    {
        private readonly WeaponManipulatorStateMachineCreator _comboCreator;
        
        private IFinishableState _combo;
        public bool IsFinished => _combo.IsFinished;

        public EquipmentManipulationState(WeaponManipulatorStateMachineCreator comboCreator) => 
            _comboCreator = comboCreator;

        public void Enter()
        {
            _combo = _comboCreator.GetCombo();
            (_combo as IEnterable)?.Enter();
        }

        public void Exit() => 
            (_combo as IExitable)?.Exit();

        public void Tick() => 
            (_combo as ITickable)?.Tick();
    }
}