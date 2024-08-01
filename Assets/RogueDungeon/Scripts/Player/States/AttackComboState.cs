using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
{
    public class AttackComboState : StateWithHandlers, IFinishableState
    {
        private readonly TestComboCreator _comboCreator;
        
        private IFinishableState _combo;
        public bool IsFinished => _combo.IsFinished;

        public AttackComboState(TestComboCreator comboCreator) => 
            _comboCreator = comboCreator;

        public override void Enter()
        {
            base.Enter();
            _combo = _comboCreator.GetCombo();
            (_combo as IEnterable)?.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            (_combo as IExitable)?.Exit();
        }

        public override void Tick()
        {
            base.Tick();
            (_combo as ITickable)?.Tick();
        }
    }
}