using Common.Fsm;
using ILogger = Common.Fsm.ILogger;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeBehaviour : StateMachine
    {
        public DodgeBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<DodgeIdleState>();
    }
}