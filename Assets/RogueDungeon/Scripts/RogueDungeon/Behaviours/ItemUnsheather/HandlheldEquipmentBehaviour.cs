using Common.Fsm;
using RogueDungeon.Items;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class HandheldItemBehaviour : StateMachine, IChangingHandheldItemsInfo
    {
        public IHandheldItem CurrentItem { get; set; }
        public IHandheldItem IntendedItem { get; set; }

        public HandheldItemBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<EvaluateState>();
    }
}