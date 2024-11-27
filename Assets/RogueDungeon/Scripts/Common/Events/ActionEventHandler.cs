using System;

namespace Common.Events
{
    public readonly struct ActionEventHandler<T> : IEventHandler<T>
    {
        private readonly Action<T> _action;

        public ActionEventHandler(Action<T> action) =>
            _action = action;

        public void HandleEvent(T @event) =>
            _action?.Invoke(@event);

        public override bool Equals(object obj) =>
            obj switch
            {
                ActionEventHandler<T> otherHandler => Equals(otherHandler),
                Action<T> action => Equals(action),
                _ => false
            };

        public bool Equals(ActionEventHandler<T> other) =>
            _action == other._action;

        public bool Equals(Action<T> action) =>
            _action == action;

        public override int GetHashCode() =>
            _action?.GetHashCode() ?? 0;

        public static bool operator ==(ActionEventHandler<T> left, ActionEventHandler<T> right) =>
            left.Equals(right);

        public static bool operator !=(ActionEventHandler<T> left, ActionEventHandler<T> right) =>
            !(left == right);

        public static bool operator ==(ActionEventHandler<T> handler, Action<T> action) =>
            handler.Equals(action);

        public static bool operator !=(ActionEventHandler<T> handler, Action<T> action) =>
            !(handler == action);

        public static bool operator ==(Action<T> action, ActionEventHandler<T> handler) =>
            handler == action;

        public static bool operator !=(Action<T> action, ActionEventHandler<T> handler) =>
            !(handler == action);
    }
}