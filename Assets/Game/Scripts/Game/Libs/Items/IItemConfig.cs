using System;

namespace Game.Libs.Items
{
    public interface IItemConfig
    {
        string Id { get; }
        Type Type { get; }
    }
}