using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public interface IItemParent
    {
        Transform ParentObject { get; }
        float CellSize { get; }
    }
}