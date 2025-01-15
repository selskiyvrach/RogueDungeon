using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Fsm;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
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
            _currentItemVisibleSetter.IsVisible = true;
            _currentItemUsableSetter.IsUsable = false;
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if (!IsFinished)
                return;
            _currentItemUsableSetter.IsUsable = true;
            stateChanger.To<EvaluateState>();
        }
    }
}