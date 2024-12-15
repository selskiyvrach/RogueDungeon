using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class Behaviour : StateMachine, IComboCounter
    {
        int IComboCounter.AttackIndex { get; set; }

        public Behaviour(IStatesFactory statesFactory) : base(statesFactory)
        {
        }

        protected override void ToStartState() => 
            To<IdleState>();
    }
}