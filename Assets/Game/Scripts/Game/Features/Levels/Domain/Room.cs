using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public class Room : IRoom
    {
        private readonly RoomConfig _config;
        private readonly ICombatRoomEncounterProvider _encounterProvider;

        public Vector2Int Coordinates => _config.Coordinates;
        public AdjacentRooms AdjacentRooms { get; set; }
        public bool CanLeave { get; private set; }
        public RoomGameObject GameObject { get; }

        public Room(RoomConfig config, ICombatRoomEncounterProvider encounterProvider, RoomGameObject gameObject)
        {
            _config = config;
            _encounterProvider = encounterProvider;
            GameObject = gameObject;
            GameObject.transform.localPosition = new Vector3(Coordinates.x, 0, Coordinates.y);
        }

        public void Initialize()
        {
        }

        public void Enter()
        {
            CanLeave = false;
            if(!_encounterProvider.TryEnter(Coordinates, FinishEncounter))
                FinishEncounter();
        }

        private void FinishEncounter() => 
            CanLeave = true;

        public void Exit()
        {
        }

        public void Tick(float timeDelta)
        {
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}