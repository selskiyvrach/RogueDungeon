using Common.Fsm;
using RogueDungeon.Items.Weapons;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class WeaponBehaviour : StateMachine, IComboCounter, IComboInfo
    {
        int IComboCounter.AttackIndex { get; set; }
        public IWeaponInfo WeaponInfo { get; set; }
        public AttackDirection[] AttackDirectionsInCombo => WeaponInfo.AttackDirectionsInCombo;

        public WeaponBehaviour(IStatesFactory statesFactory) : base(statesFactory)
        {
        }

        protected override void ToStartState() => 
            To<IdleState>();
    }
}