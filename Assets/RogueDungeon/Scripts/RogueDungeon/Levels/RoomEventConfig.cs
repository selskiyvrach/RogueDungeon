using System;
using UnityEngine;

namespace Levels
{
    public abstract class RoomEventConfig : ScriptableObject
    {
        public abstract Type EventType { get; }
    }
}