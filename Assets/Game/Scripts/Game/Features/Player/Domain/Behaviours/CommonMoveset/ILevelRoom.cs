using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public interface ILevelRoom
    {
        void Enter();
        void Exit();
        bool HasAdjacentRoom(Vector2Int direction);
    }
}