using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public interface ILevelRoomsInfoProvider
    {
        ILevelRoom GetRoom(Vector2Int coordinates);
    }
}