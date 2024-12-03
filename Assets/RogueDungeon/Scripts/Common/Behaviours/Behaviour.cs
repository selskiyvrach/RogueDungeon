using System;
using UniRx;

namespace Common.Behaviours
{
    public abstract class Behaviour : IBehaviour
    {
        private IDisposable _sub;

        public virtual void Enable()
        {
            _sub?.Dispose();
            _sub = Observable.EveryUpdate().Subscribe(_ => Tick());
        }

        public virtual void Disable() => 
            _sub?.Dispose();

        public abstract void Tick();
    }
}