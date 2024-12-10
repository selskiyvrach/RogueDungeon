using System;
using UniRx;

namespace Common.FSM
{
    public class Timer : IFinishable
    {
        private float _duration;
        private IDisposable _sub;
        private Action _callback;
        public bool IsFinished { get; private set; }

        public Timer(float duration) => 
            _duration = duration;

        public Timer()
        {
        }

        public void Start(float duration, Action callback)
        {
            _callback = callback;
            Start(duration);
        }

        public void Start(float duration)
        {
            _duration = duration;
            Start();
        }

        public void Start()
        {
            IsFinished = false;
            _sub = Observable.Timer(TimeSpan.FromSeconds(_duration)).Subscribe(_ =>
            {
                _callback?.Invoke();
                IsFinished = true;
            });
        }

        public void Cancel() => 
            _sub?.Dispose();
    }
}