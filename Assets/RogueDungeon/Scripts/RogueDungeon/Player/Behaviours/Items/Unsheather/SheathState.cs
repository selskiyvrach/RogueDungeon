using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;
using Common.Parameters;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    internal class SheathState : BoundToAnimationState, ITypeBasedTransitionableState
    {
        private readonly ICurrentItemSetter _currentItemGetter;
        private readonly ICurrentItemVisibleSetter _itemVisibleSetter;
        private readonly ICurrentItemUsableSetter _itemUsableSetter;
        private readonly IParameter<IUnsheathDuration> _timeFormula;

        protected override AnimationData Animation => new(AnimationNames.SHEATH_RIGHT_HAND, _timeFormula.Value);

        public SheathState(IAnimator animator, IParameter<IUnsheathDuration> timeFormula, ICurrentItemSetter currentItemGetter, ICurrentItemVisibleSetter itemVisibleSetter, ICurrentItemUsableSetter itemUsableSetter) : base(animator)
        {
            _timeFormula = timeFormula;
            _currentItemGetter = currentItemGetter;
            _itemVisibleSetter = itemVisibleSetter;
            _itemUsableSetter = itemUsableSetter;
        }

        public override void Enter()
        {
            _itemUsableSetter.IsUsable = false;
            base.Enter();
        }

        public void CheckTransitions(ITypeBasedStateChanger typeBasedStateChanger)
        {
            if (!IsFinished)
                return;
            _itemVisibleSetter.IsVisible = false;
            _currentItemGetter.Item = null;
            typeBasedStateChanger.ChangeState<EvaluateState>();
        }
    }
}