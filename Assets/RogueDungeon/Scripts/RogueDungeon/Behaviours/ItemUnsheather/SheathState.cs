using Common.Animations;
using Common.Fsm;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Fsm;
using RogueDungeon.Items;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class SheathState : BoundToAnimationState
    {
        private readonly IChangingHandheldItemsInfo _equipment;
        private readonly IUnsheathDuration _timeFormula;

        protected override AnimationData Animation => new(AnimationNames.SHEATH_RIGHT_HAND, _timeFormula.Value);

        public SheathState(IAnimator animator, IChangingHandheldItemsInfo equipment, IUnsheathDuration timeFormula) : base(animator)
        {
            _equipment = equipment;
            _timeFormula = timeFormula;
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