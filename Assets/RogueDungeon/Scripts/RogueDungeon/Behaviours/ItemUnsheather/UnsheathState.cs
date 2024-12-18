using Common.Animations;
using Common.Fsm;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Fsm;
using RogueDungeon.Items;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class UnsheathState : BoundToAnimationState
    {
        private readonly IChangingHandheldItemsInfo _equipment;
        private readonly IUnsheathDuration _duration;
        private IHandheldItem _itemBeingUnsheathed;

        protected override AnimationData Animation => new(AnimationNames.UNSHEATH_RIGHT_HAND, _duration.Value);

        public UnsheathState(IAnimator animator, IChangingHandheldItemsInfo equipment, IUnsheathDuration duration) : base(animator)
        {
            _equipment = equipment;
            _duration = duration;
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