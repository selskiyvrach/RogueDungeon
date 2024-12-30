using Common.Behaviours;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeExternalFacade : IBehaviourExternalFacade, ICanDodgeSetter, IDodgeStateGetter
    {
        private readonly ICanDodgeSetter _canDodgeSetter;
        private readonly IDodgeStateGetter _dodgeStateGetter;

        public DodgeState DodgeState => _dodgeStateGetter.DodgeState;
        public bool CanDodge
        {
            set => _canDodgeSetter.CanDodge = value;
        }

        public DodgeExternalFacade(ICanDodgeSetter canDodgeSetter, IDodgeStateGetter dodgeStateGetter)
        {
            _canDodgeSetter = canDodgeSetter;
            _dodgeStateGetter = dodgeStateGetter;
        }
    }
}