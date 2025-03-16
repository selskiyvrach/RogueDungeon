using System;

namespace Characters
{
    public interface IHealth
    {
        float Current { get; }
        float Max { get; }
        event Action OnChanged;
    }
}