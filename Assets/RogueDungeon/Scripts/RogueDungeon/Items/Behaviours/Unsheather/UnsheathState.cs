using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Fsm;
using RogueDungeon.Items.Behaviours.Common;

namespace RogueDungeon.Items.Behaviours.Unsheather
{
    public class UnsheathState : BoundToAnimationState
    {
        private readonly ICurrentItemVisibleSetter _currentItemVisibleSetter;
        private readonly ICurrentItemUsableSetter _currentItemUsableSetter;
        private readonly IParameter<IUnsheathDuration> _duration;

        protected override AnimationData Animation => new(AnimationNames.UNSHEATH_RIGHT_HAND, _duration.Value);

        public UnsheathState(IAnimator animator, IParameter<IUnsheathDuration> duration, ICurrentItemVisibleSetter currentItemVisibleSetter, ICurrentItemUsableSetter currentItemUsableSetter) : base(animator)
        {
            _duration = duration;
            _currentItemVisibleSetter = currentItemVisibleSetter;
            _currentItemUsableSetter = currentItemUsableSetter;
        }

        public override void Enter()
        {
            _currentItemVisibleSetter.SetVisible(true);
            _currentItemUsableSetter.SetUsable(false);
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if (!IsTimerOff)
                return;
            _currentItemUsableSetter.SetUsable(true);
            stateChanger.To<EvaluateState>();
        }
    }
}