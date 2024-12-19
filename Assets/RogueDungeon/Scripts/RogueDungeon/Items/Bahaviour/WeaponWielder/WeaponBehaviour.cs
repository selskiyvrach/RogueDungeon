using Common.Fsm;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class WeaponBehaviour : StateMachine
    {
        public WeaponBehaviour(IStatesFactory statesFactory) : base(statesFactory)
        {
        }

        protected override void ToStartState() => 
            To<IdleState>();
    }
}