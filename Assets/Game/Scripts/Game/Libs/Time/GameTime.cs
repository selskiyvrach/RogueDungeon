using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using ITickable = Libs.Lifecycle.ITickable;

namespace Game.Libs.Time
{
    public class GameTime : MonoBehaviour, IGameTime
    {
        private readonly HashSet<LifecycleTickableToTimeTickableAdapter> _tickables = new();
        private global::Libs.Time.Time _time;
        private IDisposable _sub;

        [Inject]
        public void Construct(global::Libs.Time.Time time) => 
            _time = time;

        private void Update() => 
            _time.Tick(UnityEngine.Time.deltaTime);

        public void StartTicking(ITickable tickable, TickOrder order)
        {
            var adapter = new LifecycleTickableToTimeTickableAdapter(tickable, order);
            if(!_tickables.Add(adapter))
                throw new InvalidOperationException("Cannot add tickable more than once!");
            
            _time.Register(adapter);
        }

        public void StopTicking(ITickable tickable)
        {
            var adapter = _tickables.First(n => n.TickableEquals(tickable));
            _time.Unregister(adapter);
            _time.Unregister(adapter);
        }
    }
}