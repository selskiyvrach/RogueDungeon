using Common.Behaviours;
using Common.Fsm;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class UnsheatherBehaviour : StateMachineBehaviour
    {
        public UnsheatherBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<EvaluateState>();
    }
}