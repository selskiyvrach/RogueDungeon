using System;

namespace Game.Libs.InGameResources
{
    public interface IReadOnlyResource
    {
        float Current { get; }
        float Max { get; }
        event Action OnChanged;
    }
}