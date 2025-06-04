using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public interface ILevelTraverser : ICurrentRoomCanLeaveReader
    {
        Vector2 BlendedGridPosition { get; set; }
        Vector2 RealRotation { get; set; }
        Vector2Int GridPosition { get; set; }
        Vector2Int GridRotation { get; set; }
        void OnExitingRoom(Vector2Int coordinates);
        void OnEnteringRoom(Vector2Int coordinates);
    }
}