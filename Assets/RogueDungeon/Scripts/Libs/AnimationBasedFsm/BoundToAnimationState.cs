using Libs.Animations;
using Libs.Fsm;
using Libs.Lifecycle;

namespace Libs.AnimationBasedFsm
{
    public abstract class BoundToAnimationState : IState, IEnterableState, IExitableState, ITickable, IFinishable
    {
        protected abstract IAnimation Animation { get; }
        protected virtual bool IsLooping { get; }
        public bool IsFinished => !IsLooping && Animation.IsFinished;
        protected abstract float Duration { get; }

        public virtual void Enter()
        {
            Animation.OnEvent += OnAnimationEvent;
            Animation.Play();
        }

        public virtual void Exit() => 
            Animation.OnEvent -= OnAnimationEvent;

        public virtual void Tick(float timeDelta)
        {
            if(IsLooping && Animation.IsFinished)
                Animation.Play();
            
            // animation runs in normalized time so controlling entity needs to convert delta to match intended duration
            var convertedDelta = 1 / Duration * timeDelta;
            Animation.TickNormalizedTime(convertedDelta);
        }

        protected abstract void OnAnimationEvent(string name);
    }
}