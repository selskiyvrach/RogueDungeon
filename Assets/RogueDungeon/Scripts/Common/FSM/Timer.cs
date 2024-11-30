using System;
using UniRx;

namespace Common.FSM
{
    public class Timer : IFinishable
    {
        private readonly float _duration;
        private IDisposable _sub;
        public bool IsFinished { get; private set; }

        public Timer(float duration) => 
            _duration = duration;

        public Timer()
        {
        }

        public void Start(float duration) => 
            _sub = Observable.Timer(TimeSpan.FromSeconds(_duration)).Subscribe(_ => Stop());

        public void Start() => 
            _sub = Observable.Timer(TimeSpan.FromSeconds(_duration)).Subscribe(_ => Stop());

        public void Stop()
        {
            _sub?.Dispose();
            IsFinished = true;
        }
    }
}