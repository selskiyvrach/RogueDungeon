using Common.Animations;
using Common.Fsm;
using Common.Lifecycle;

namespace Common.AnimationBasedFsm
{
    public abstract class BoundToAnimationState : IState, IEnterableState, IExitableState, ITickable, IFinishable
    {
        protected abstract IAnimation Animation   { get; }
        protected virtual bool IsLooping { get; }
        public bool IsFinished => !IsLooping && Animation.IsFinished;

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
            
            Animation.Tick(timeDelta);
        }

        protected abstract void OnAnimationEvent(string name);
    }
}