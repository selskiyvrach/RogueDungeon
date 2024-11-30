using System;
using RogueDungeon.Entities.Properties;

namespace Common.FSM
{
    public static class StateExtensions
    {
        public static T Bind<T>(this T state, IProperty<bool> value) where T : State
        {
            state.Bind(n => value.Value = n);
            return state;
        }

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
    }
}