using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public interface IRoomsInfoProvider
    {
        bool RoomExists(Vector2Int coordinates);
    }
}