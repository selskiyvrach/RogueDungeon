using System;

namespace Characters
{
    public interface IReadOnlyResource
    {
        float Current { get; }
        float Max { get; }
        event Action OnChanged;
    }
}