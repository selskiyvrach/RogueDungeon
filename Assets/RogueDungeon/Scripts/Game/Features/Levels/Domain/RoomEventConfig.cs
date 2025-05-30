using System;
using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public abstract class RoomEventConfig : ScriptableObject
    {
        public abstract Type EventType { get; }
    }
}