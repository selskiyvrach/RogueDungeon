using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Fsm;
using RogueDungeon.Items;
using RogueDungeon.Parameters;

namespace RogueDungeon.Behaviours.EquipmentBehaviour
{
    public class UnsheathState : BoundToAnimationState
    {
        private readonly ICurrentEquipmentState _equipment;
        private IItemHandle _itemBeingUnsheathed;

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