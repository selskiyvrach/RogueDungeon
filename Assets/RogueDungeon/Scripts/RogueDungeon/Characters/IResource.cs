using System;

namespace Characters
{
    public interface IResource
    {
        float Current { get; }
        float Max { get; }
        event Action OnChanged;
    }
}