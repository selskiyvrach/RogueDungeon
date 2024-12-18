using Common.Fsm;

namespace RogueDungeon.Items.Handling.Unsheather
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