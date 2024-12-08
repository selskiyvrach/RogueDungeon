using System;
using Common.Properties;
using UnityEngine;

namespace Common.FSM
{
    public static class StateExtensions
    {
        public static T OnEnter<T>(this T state, Action action) where T : State
        {
            state.AddEnterHandler(action);
            return state;
        }
        
        public static T OnExit<T>(this T state, Action action) where T : State
        {
            state.AddExitHandler(action);
            return state;
        }
        
        public static T LogEnter<T>(this T state, string log) where T : State
        {
            state.AddExitHandler(() => Debug.Log(log));
            return state;
        }
    }
}