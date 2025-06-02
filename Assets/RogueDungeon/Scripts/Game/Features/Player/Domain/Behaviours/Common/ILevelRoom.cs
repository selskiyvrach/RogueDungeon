using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public interface ILevelRoom
    {
        void Enter();
        void Exit();
        bool HasAdjacentRoom(Vector2Int direction);
    }
}