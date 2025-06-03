using UnityEngine;

namespace Game.Libs.WorldObjects
{
    public interface ITwoDWorldObject
    {
        Vector2 LocalPosition { get; set; }
        Vector2 Rotation2D { get; set; }
    }
}