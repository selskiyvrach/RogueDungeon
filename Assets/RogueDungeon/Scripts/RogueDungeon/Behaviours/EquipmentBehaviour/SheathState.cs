using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Fsm;
using RogueDungeon.Parameters;

namespace RogueDungeon.Behaviours.EquipmentBehaviour
{
    public class SheathState : BoundToAnimationState
    {
        private readonly ICurrentEquipmentState _equipment;
        private readonly IParameters _parameters;

        protected override AnimationData Animation => new(AnimationNames.SHEATH_RIGHT_HAND, _parameters.Get(ParameterKeys.SHEATH_RIGHT_HAND_DURATION));

        public SheathState(IAnimator animator, IParameters parameters, ICurrentEquipmentState equipment) : base(animator)
        {
            _parameters = parameters;
            _equipment = equipment;
        }

        public override void Enter()
        {
            _equipment.CurrentItem.SetEnabled(false);
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if (!IsTimerOff)
                return;
            _equipment.CurrentItem.SetVisible(false);
            _equipment.CurrentItem = null;
            stateChanger.To<EvaluateState>();
        }
    }
}