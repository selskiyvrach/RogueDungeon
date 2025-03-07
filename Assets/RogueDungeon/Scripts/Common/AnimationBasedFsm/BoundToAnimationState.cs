using Common.Animations;
using Common.Fsm;

namespace Common.AnimationBasedFsm
{
    public abstract class BoundToAnimationState : IState, IEnterableState, IExitableState
    {
        protected abstract IAnimation Animation { get; }
        public bool IsFinished => Animation.IsFinished;

        public virtual void Enter()
        {
            Animation.OnEvent += OnAnimationEvent;
            Animation.Play();
        }

        public virtual void Exit()
        {
            Animation.OnEvent -= OnAnimationEvent;
            Animation.Stop();
        }

        protected abstract void OnAnimationEvent(string name);
    }
}