using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class LevelRoomsInfoContext : ILevelRoomsInfoProvider
    {
        public IReadOnlyDictionary<Vector2Int, ILevelRoom> Rooms { get; set; } 
        
        public ILevelRoom GetRoom(Vector2Int coordinates) => 
            Rooms[coordinates];
    }
}