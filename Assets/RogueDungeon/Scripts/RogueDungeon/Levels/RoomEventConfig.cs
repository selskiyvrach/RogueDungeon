using System;
using UnityEngine;

namespace RogueDungeon.Levels
{
    public abstract class RoomEventConfig : ScriptableObject
    {
        public abstract Type EventType { get; }
    }
}