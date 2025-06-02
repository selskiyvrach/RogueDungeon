using System;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public interface IRoom : IInitializable, IDisposable, ITickable
    {
        Vector2Int Coordinates { get; }
        AdjacentRooms AdjacentRooms { get; }
        bool CanLeave { get; }
        void Enter();
        void Exit();
    }
}