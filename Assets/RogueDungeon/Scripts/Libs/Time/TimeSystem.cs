using System;
using System.Collections.Generic;
using System.Linq;

namespace Libs.Time
{
    public class TimeSystem : ITimeService
    {
        private readonly SortedDictionary<int, List<ITickable>> _tickables = new();
        private readonly List<ITickable> _callBuffer = new(100);
        private readonly HashSet<ITickable> _uniquelyAddedTickables = new();
        
        public void Tick(float deltaTime)
        {
            _callBuffer.AddRange(_tickables.SelectMany(n => n.Value));
            for (var i = 0; i < _callBuffer.Count; i++) 
                _callBuffer[i]?.Tick(deltaTime);
            _callBuffer.Clear();
        }

        public void Register(ITickable tickable)
        {
            if (!_uniquelyAddedTickables.Add(tickable))
                throw new Exception("This tickable has already been registered.");
            
            _tickables.TryAdd(tickable.TickOrder, new List<ITickable>());
            _tickables[tickable.TickOrder].Add(tickable);
        }

        public bool Unregister(ITickable tickable)
        {
            if(!_tickables.TryGetValue(tickable.TickOrder, out var tickables) || !tickables.Remove(tickable))
                return false;
            
            for (var i = 0; i < _callBuffer.Count; i++)
            {
                if (_callBuffer[i] != tickable)
                    continue;
                _callBuffer[i] = null;
                break;
            }
            
            _uniquelyAddedTickables.Remove(tickable);
            return true;
        }
    }
}