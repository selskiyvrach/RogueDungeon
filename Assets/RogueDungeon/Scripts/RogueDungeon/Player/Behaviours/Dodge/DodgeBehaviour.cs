using Common.Behaviours;
using Common.Fsm;
using ILogger = Common.Fsm.ILogger;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeBehaviour : StateMachineBehaviour
    {
        public DodgeBehaviour(ITypeBasedStatesProvider statesProvider, ILogger logger = null) : base(statesProvider, logger)
        {
        }

        protected override void ToStartState() => 
            ChangeState<DodgeIdleState>();
    }
}