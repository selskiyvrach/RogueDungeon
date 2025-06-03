using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public interface ILevelRoomsInfoProvider
    {
        ILevelRoom GetRoom(Vector2Int coordinates);
    }
}