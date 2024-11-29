using Common.Providers;

namespace Common.FSM
{
    public static class StateExtensions
    {
        public static T Bind<T>(this T state, IValue<bool> value) where T : State
        {
            state.Bind(value);
            return state;
        }
    }
}