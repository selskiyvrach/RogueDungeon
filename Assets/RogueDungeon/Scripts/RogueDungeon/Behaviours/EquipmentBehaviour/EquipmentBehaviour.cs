using Common.Fsm;

namespace RogueDungeon.Behaviours.EquipmentBehaviour
{
    public class EquipmentBehaviour : StateMachine
    {
        public EquipmentBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState()
        {
            throw new System.NotImplementedException();
        }
    }
}