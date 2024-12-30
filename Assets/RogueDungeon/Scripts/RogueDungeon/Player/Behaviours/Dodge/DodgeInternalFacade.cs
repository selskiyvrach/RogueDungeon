using Common.Behaviours;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeInternalFacade : IBehaviourInternalFacade, ICanDodgeGetter, ICanDodgeSetter, IDodgeStateSetter, IDodgeStateGetter
    {
        public bool CanDodge { get; set; } = true;
        public DodgeState DodgeState { get; set; }
    }
}