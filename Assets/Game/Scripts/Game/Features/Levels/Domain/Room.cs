using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public class Room : IRoom
    {
        private readonly IRoomConfig _config;

        public Vector2Int Coordinates => _config.Coordinates;
        public bool CanLeave { get; private set; }
        
        public Room(IRoomConfig config)
        {
            _config = config;
        }

        public void Enter()
        {
            CanLeave = false;
            FinishEncounter();
        }

        private void FinishEncounter() => 
            CanLeave = true;

        public void Exit()
        {
        }
    }
}