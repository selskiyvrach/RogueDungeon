using System.Collections.Generic;
using JetBrains.Annotations;
using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon
{
    public interface ISurroundingCharactersProvider
    {
        IReadOnlyList<Character> AllCharacters { get; }
        [CanBeNull] Character GetTargetForPosition(Positions positions);
    }

    public interface ISurroundingsProvider
    {
        Vector3 GetWorldCoordinatesForPosition(Positions positions);
    }
}