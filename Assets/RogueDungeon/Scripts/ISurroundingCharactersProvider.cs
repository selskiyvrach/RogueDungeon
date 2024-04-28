using System.Collections.Generic;
using JetBrains.Annotations;
using RogueDungeon.Characters;

namespace RogueDungeon
{
    public interface ISurroundingCharactersProvider
    {
        IReadOnlyList<Character> AllCharacters { get; }
        [CanBeNull] Character GetTargetForPosition(Position position);
    }
}