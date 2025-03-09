using Common.Animations;
using Common.Fsm;
using Common.Lifecycle;

namespace Common.AnimationBasedFsm
{
    public abstract class BoundToAnimationState : IState, IEnterableState, IExitableState, ITickable
    {
        protected abstract IAnimation Animation { get; }
        public bool IsFinished => Animation.IsFinished;

        public virtual void Enter()
        {
            Animation.OnEvent += OnAnimationEvent;
            Animation.Play();
        }

        public virtual void Exit() => 
            Animation.OnEvent -= OnAnimationEvent;

        public virtual void Tick(float timeDelta) => 
            Animation.Tick(timeDelta);

        protected abstract void OnAnimationEvent(string name);
    }
}