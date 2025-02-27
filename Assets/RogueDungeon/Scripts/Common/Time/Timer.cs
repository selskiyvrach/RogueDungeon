using System;
using UniRx;

namespace Common.Time
{
    public class Timer
    {
        private IDisposable _sub;

        public bool IsFinished { get; private set; } = true;

        public void Start(float time, Action callback = null)
        {
            IsFinished = false;
            _sub = Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(_ =>
            {
                IsFinished = true;
                callback?.Invoke();
            });
        }

        public void Stop() => 
            _sub?.Dispose();
    }
}