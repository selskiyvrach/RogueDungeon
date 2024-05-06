using JetBrains.Annotations;
using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon
{
    public interface ISurroundingCharactersProvider
    {
        [CanBeNull] Character GetTargetForPosition(Position position);
    }

    public interface ISurroundingsProvider
    {
        Vector3 GetWorldCoordinatesForPosition(Position position);
    }
}