using Common.Behaviours;
using Common.Fsm;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponWielderBehaviour : StateMachineBehaviour
    {
        public WeaponWielderBehaviour(IStatesFactory statesFactory) : base(statesFactory)
        {
        }

        protected override void ToStartState() => 
            To<IdleState>();
    }
}