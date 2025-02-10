using Common.Behaviours;
using Common.Fsm;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class UnsheatherBehaviour : StateMachineBehaviour
    {
        public UnsheatherBehaviour(ITypeBasedStatesProvider statesProvider, ILogger logger = null) : base(statesProvider, logger)
        {
        }

        protected override void ToStartState() => 
            ChangeState<EvaluateState>();
    }
}