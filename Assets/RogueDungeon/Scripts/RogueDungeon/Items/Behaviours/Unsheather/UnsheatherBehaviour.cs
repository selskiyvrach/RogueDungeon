using Common.Fsm;

namespace RogueDungeon.Items.Behaviours.Unsheather
{
    public class UnsheatherBehaviour : StateMachine
    {
        public UnsheatherBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<EvaluateState>();
    }
}