using Common.Fsm;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public class WeaponWielderBehaviour : StateMachine
    {
        public WeaponWielderBehaviour(IStatesFactory statesFactory) : base(statesFactory)
        {
        }

        protected override void ToStartState() => 
            To<IdleState>();
    }
}