using System;
using UniRx;
using UnityEngine;

namespace Common.Behaviours
{
    public class Ticker
    {
        private IDisposable _sub;

        public void Start(Action<float> onTick)
        {
            _sub?.Dispose();
            _sub = Observable.EveryUpdate().Subscribe(_ => onTick(Time.deltaTime));
        }

        public void Stop() => 
            _sub?.Dispose();
    }
}