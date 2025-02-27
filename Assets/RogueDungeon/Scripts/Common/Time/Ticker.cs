using System;
using UniRx;

namespace Common.Time
{
    public class Ticker
    {
        private IDisposable _sub;

        public void Start(Action<float> onTick)
        {
            _sub?.Dispose();
            _sub = Observable.EveryUpdate().Subscribe(_ => onTick(UnityEngine.Time.deltaTime));
        }

        public void Stop() => 
            _sub?.Dispose();
    }
}