using System;
using UnityEngine;

namespace Game.Libs.Items
{
    public interface IItemConfig
    {
        string Id { get; }
        Type Type { get; }
        Vector2Int Size { get; }
    }
}