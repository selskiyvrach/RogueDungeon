using System;

namespace Common.FSM
{
    public interface ICompetingState<out T> where T : IComparable<T>
    {
        T Competition { get; }
    }

    public interface ICompetingState
    {
    }
}