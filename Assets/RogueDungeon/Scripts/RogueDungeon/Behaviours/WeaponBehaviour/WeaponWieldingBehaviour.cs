using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal class WeaponWieldingBehaviour : StateMachine
    {
        public WeaponWieldingBehaviour(IStatesFactory statesFactory) : base(statesFactory)
        {
            
        }

        protected override void ToStartState() => 
            To<IdleState>();
    }
}