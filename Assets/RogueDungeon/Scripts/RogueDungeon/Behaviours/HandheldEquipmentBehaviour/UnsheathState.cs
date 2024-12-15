using Common.Animations;
using Common.Fsm;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Fsm;
using RogueDungeon.Items;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class UnsheathState : BoundToAnimationState
    {
        private readonly ICurrentEquipmentState _equipment;
        private IHandheldItem _itemBeingUnsheathed;

        protected override AnimationData Animation => new(AnimationNames.UNSHEATH_RIGHT_HAND, _itemBeingUnsheathed.UnsheathDuration);

        public UnsheathState(IAnimator animator, ICurrentEquipmentState equipment) : base(animator)
        {
            _equipment = equipment;
        }

        public override void Enter()
        {
            _itemBeingUnsheathed = _equipment.IntendedItem;
            _itemBeingUnsheathed.SetVisible(true);
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if (!IsTimerOff)
                return;
            _equipment.CurrentItem = _itemBeingUnsheathed;
            _itemBeingUnsheathed.SetEnabled(true);
            stateChanger.To<EvaluateState>();
        }
    }
}