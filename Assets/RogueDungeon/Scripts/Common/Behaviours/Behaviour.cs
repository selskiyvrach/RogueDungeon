using System;
using Common.FSM;
using UniRx;

namespace Common.Behaviours
{
    public abstract class Behaviour : IBehaviour
    {
        private IDisposable _sub;

        protected virtual ITickable Tickable { get; }

        public virtual void Enable()
        {
            _sub?.Dispose();
            _sub = Observable.EveryUpdate().Subscribe(_ => Tick());
        }

        public void Disable() => 
            _sub?.Dispose();

        private void Tick() => 
            Tickable?.Tick();
    }
}