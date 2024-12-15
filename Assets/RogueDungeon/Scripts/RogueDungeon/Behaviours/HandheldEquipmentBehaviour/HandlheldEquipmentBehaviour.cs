using Common.Fsm;
using RogueDungeon.Items;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class EquipmentBehaviour : StateMachine, ICurrentEquipmentState
    {
        public IHandheldItem CurrentItem { get; set; }
        public IHandheldItem IntendedItem { get; set; }

        public EquipmentBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<EvaluateState>();
    }
}