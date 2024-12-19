using Common.Animations;
using Common.Fsm;
using RogueDungeon.Fsm;
using RogueDungeon.Items.Bahaviour.Common;
using RogueDungeon.Items.Bahaviour.WeaponWielder;

namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    internal class SheathState : BoundToAnimationState
    {
        private readonly ICurrentItemSetter _currentItemGetter;
        private readonly ICurrentItemVisibleSetter _itemVisibleSetter;
        private readonly ICurrentItemUsableSetter _itemUsableSetter;
        private readonly IUnsheathDuration _timeFormula;

        protected override AnimationData Animation => new(AnimationNames.SHEATH_RIGHT_HAND, _timeFormula.Value);

        public SheathState(IAnimator animator, IUnsheathDuration timeFormula, ICurrentItemSetter currentItemGetter, ICurrentItemVisibleSetter itemVisibleSetter, ICurrentItemUsableSetter itemUsableSetter) : base(animator)
        {
            _timeFormula = timeFormula;
            _currentItemGetter = currentItemGetter;
            _itemVisibleSetter = itemVisibleSetter;
            _itemUsableSetter = itemUsableSetter;
        }

        public override void Enter()
        {
            _itemUsableSetter.SetUsable(false);
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if (!IsTimerOff)
                return;
            _itemVisibleSetter.SetVisible(false);
            _currentItemGetter.Item = null;
            stateChanger.To<EvaluateState>();
        }
    }
}