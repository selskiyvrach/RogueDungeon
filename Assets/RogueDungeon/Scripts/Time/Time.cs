using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Time
{
    public class Time : MonoBehaviour
    {
        private static Time _instance;
        private static readonly List<(bool delete, Action<float> callback, float duration, float timeLeft)> _timeProgressListeners = new();
        private static readonly List<(bool delete, Action callback, float timeLeft)> _scheduledCallbacks = new();

        private void Awake()
        {
            _instance = _instance == null
                ? this
                : throw new InvalidOperationException("Time instance is already created");
            DontDestroyOnLoad(gameObject);
        }

        public static void Schedule(Action callback, float dueTime) => 
            _scheduledCallbacks.Add((false, callback, dueTime));

        public static void Unschedule(Action callback)
        {
            for (var i = 0; i < _scheduledCallbacks.Count; i++)
            {
                var unschedule = _scheduledCallbacks[i];
                if (unschedule.callback != callback)
                    continue;
                unschedule.delete = true;
                _scheduledCallbacks[i] = unschedule;
            }
        }

        private void Update()
        {
            var timeDelta = UnityEngine.Time.deltaTime;
            
            for (var i = _scheduledCallbacks.Count - 1; i >= 0; i--)
            {
                var callback = _scheduledCallbacks[i];
                if (callback.delete)
                {
                    _scheduledCallbacks.RemoveAt(i);
                    continue;
                }

                callback.timeLeft -= timeDelta;
                if (callback.timeLeft <= 0)
                {
                    callback.callback?.Invoke();
                    _scheduledCallbacks.RemoveAt(i);
                    continue;
                }
                _scheduledCallbacks[i] = callback;
            }
        }
    }
}