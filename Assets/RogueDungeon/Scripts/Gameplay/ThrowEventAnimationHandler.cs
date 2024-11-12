using System;
using RogueDungeon.Animations;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using UniRx;

namespace RogueDungeon.Gameplay
{
    public abstract class ThrowAnimationEventStateHandler : IStateEnterHandler, IStateExitHandler 
    {
        private readonly IAnimation _animation;
        private readonly (string _eventName, Action _dispatcher)[] _eventDispatchers;
        private IDisposable _sub;

        protected ThrowAnimationEventStateHandler(IAnimation animation, IEventBus<IAnimationEvent> eventBus, (string eventName, Action dispatcher)[] eventHandlers)
        {
            _eventDispatchers = eventHandlers;
            _animation = animation;
        }

        public void OnEnter()
        {
            _sub = _animation.OnEvent.Subscribe(name =>
            {
                foreach (var (eventName, dispatcher) in _eventDispatchers)
                {
                    if(name == eventName)
                        dispatcher?.Invoke();
                }
            });
        }

        public void OnExit() => 
            _sub?.Dispose();
    }
    
    
    public class ThrowAnimationEventStateHandler<T> : ThrowAnimationEventStateHandler where T : IAnimationEvent, new()
    {
        public ThrowAnimationEventStateHandler(IAnimation animation, IEventBus<IAnimationEvent> eventBus, string eventName) : base(animation, eventBus, new (string, Action)[]
            { (eventName, () => eventBus.Fire(new T())) })
        {
        }
    }
    
    public class ThrowAnimationEventStateHandler<T1, T2> : ThrowAnimationEventStateHandler where T1 : IAnimationEvent, new() where T2 : IAnimationEvent, new()
    {
        public ThrowAnimationEventStateHandler(IAnimation animation, IEventBus<IAnimationEvent> eventBus, string event1Name, string event2Name) : base(animation, eventBus, new (string, Action)[]
        {
            (event1Name, () => eventBus.Fire(new T1())),
            (event2Name, () => eventBus.Fire(new T2())),
        })
        {
        }
    }
}