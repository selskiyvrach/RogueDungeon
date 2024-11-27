using System;

namespace Common.FSM
{
    public class CompetingState<T> : State, ICompetingState, ICompetingState<T> where T : IComparable<T>
    {
        public T Competition { get; set; }
    }
}