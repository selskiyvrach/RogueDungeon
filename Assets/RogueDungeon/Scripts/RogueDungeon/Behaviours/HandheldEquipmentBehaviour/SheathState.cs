using Common.Animations;
using Common.Fsm;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Fsm;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class SheathState : BoundToAnimationState
    {
        private readonly ICurrentEquipmentState _equipment;

        protected override AnimationData Animation => new(AnimationNames.SHEATH_RIGHT_HAND, _equipment.CurrentItem.SheathDuration);

        public SheathState(IAnimator animator, ICurrentEquipmentState equipment) : base(animator) => 
            _equipment = equipment;

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