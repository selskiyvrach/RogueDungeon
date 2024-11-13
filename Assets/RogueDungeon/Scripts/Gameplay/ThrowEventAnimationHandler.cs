using System;
using RogueDungeon.Animations;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using UniRx;

namespace RogueDungeon.Gameplay
{
    public abstract class AnimationEventStateHandler : IStateEnterHandler, IStateExitHandler 
    {
        private readonly IAnimation _animation;
        private readonly string _eventName;
        private IDisposable _sub;

        protected AnimationEventStateHandler(IAnimation animation, string eventName)
        {
            _animation = animation;
            _eventName = eventName;
        }

        public void OnEnter()
        {
            _sub = _animation.OnEvent.Subscribe(name =>
            {
                if(name == _eventName)
                    OnEvent();
            });
        }

        public void OnExit() => 
            _sub?.Dispose();

        protected abstract void OnEvent();
    }
    
    
    public class AnimationEventStateHandler<T> : AnimationEventStateHandler where T : IAnimationEvent, new()
    {
        private readonly IEventBus<IAnimationEvent> _eventBus;
        private readonly T _event;

        public AnimationEventStateHandler(IEventBus<IAnimationEvent> eventBus, IAnimation animation, string eventName, T @event) : base(animation, eventName)
        {
            _eventBus = eventBus;
            _event = @event;
        }

        protected override void OnEvent() => 
            _eventBus.Fire(_event);
    }
}