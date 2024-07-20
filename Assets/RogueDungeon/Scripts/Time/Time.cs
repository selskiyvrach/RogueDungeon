using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Time
{
    public class Time : MonoBehaviour, ITime
    {
        private readonly SortedList<int, Action> _tickListeners = new();
        private readonly List<(int order, Action callback)> _waitingToBeAdded = new();

        public float TimeDelta => UnityEngine.Time.deltaTime;

        private void Update()
        {
            for (var i = _tickListeners.Count - 1; i >= 0; i--)
            {
                if(_tickListeners[i] != null)
                    _tickListeners[i].Invoke();
            }

            foreach (var (order, callback) in _waitingToBeAdded) 
                _tickListeners.Add(order, callback);
            
            _waitingToBeAdded.Clear();
        }

        public void EachTick(Action callback, int tickOrder) => 
            _waitingToBeAdded.Add((tickOrder, callback));

        public void StopEachTick(Action callback)
        {
            for (var i = 0; i < _tickListeners.Count; i++)
            {
                if (_tickListeners[i] != callback)
                    continue;
                    
                _tickListeners[i] = null;
                return;
            }

            for (var i = _waitingToBeAdded.Count - 1; i >= 0; i--)
            {
                if (_waitingToBeAdded[i].callback != callback)
                    continue;
                _waitingToBeAdded.RemoveAt(i);
                break;
            }
        }
    }
}