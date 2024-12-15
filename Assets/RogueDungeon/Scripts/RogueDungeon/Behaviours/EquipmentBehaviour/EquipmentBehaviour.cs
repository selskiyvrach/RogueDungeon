using Common.Fsm;

namespace RogueDungeon.Behaviours.EquipmentBehaviour
{
    public class EquipmentBehaviour : StateMachine, ICurrentEquipmentState
    {
        public IItemHandle CurrentItem { get; set; }
        public IItemHandle IntendedItem { get; set; }

        public EquipmentBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<EvaluateState>();
    }
}