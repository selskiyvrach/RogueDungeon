using UnityEngine;

namespace Game.Features.Player.Domain.Movesets.Movement
{
    public interface IRoomsInfoProvider
    {
        bool RoomExists(Vector2Int coordinates);
    }
}