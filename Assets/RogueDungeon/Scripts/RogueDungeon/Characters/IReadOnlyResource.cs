using System;

namespace RogueDungeon.Characters
{
    public interface IReadOnlyResource
    {
        float Current { get; }
        float Max { get; }
        event Action OnChanged;
    }
}