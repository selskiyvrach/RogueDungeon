using UnityEngine;

namespace RogueDungeon.Levels
{
    public interface ILevelTraverser
    {
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
    }
}