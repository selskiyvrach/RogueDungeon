using System;
using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public interface ICombatRoomEncounterProvider
    {
        bool TryEnter(Vector2Int roomCoordinates, Action onFinished);
    }
}