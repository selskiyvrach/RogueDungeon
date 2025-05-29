using System;

namespace InGameResources
{
    public interface IReadOnlyResource
    {
        float Current { get; }
        float Max { get; }
        event Action OnChanged;
    }
}