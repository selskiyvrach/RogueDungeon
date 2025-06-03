using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public interface ILevelTraverser : ICurrentRoomCanLeaveReader
    {
        Vector2 RealPosition { set; }
        Vector2 RealRotation { set; }
        Vector2Int GridPosition { get; set; }
        Vector2Int GridRotation { get; }
    }
}