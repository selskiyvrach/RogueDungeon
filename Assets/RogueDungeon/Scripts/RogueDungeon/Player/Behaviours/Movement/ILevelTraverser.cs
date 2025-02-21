using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public interface ILevelTraverser
    {
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
    }
}