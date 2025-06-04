using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public interface IRoom
    {
        Vector2Int Coordinates { get; }
        bool CanLeave { get; }
        void Enter();
        void Exit();
    }
}