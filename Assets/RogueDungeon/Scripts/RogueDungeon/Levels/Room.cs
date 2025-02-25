using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Unity;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Levels
{
    public class Room : IRoom
    {
        private readonly IFactory<RoomEventConfig, IRoomEvent> _roomEventFactory;
        private readonly RoomConfig _config;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SortedList<RoomEventPriority, IRoomEvent> _events = new();
        private Coroutine _coroutine;

        public Vector2Int Coordinates => _config.Coordinates;
        public AdjacentRooms AdjacentRooms { get; set; }

        public Room(RoomConfig config, ICoroutineRunner coroutineRunner, IFactory<RoomEventConfig, IRoomEvent> roomEventFactory)
        {
            _config = config;
            _coroutineRunner = coroutineRunner;
            _roomEventFactory = roomEventFactory;
        }

        public void Initialize() => 
            _config.EventConfigs.ForEach(n => AddEvent(_roomEventFactory.Create(n)));

        public void Enter()
        {
            if(_coroutine != null)
                throw new InvalidOperationException("Room already entered.");
            _coroutine = _coroutineRunner.Run(ProcessRoomEvents());
        }

        public void Exit() => 
            RemoveRoutine();

        public void AddEvent(IRoomEvent roomEvent) => 
            _events.Add(roomEvent.Priority, roomEvent);

        private IEnumerator ProcessRoomEvents()
        {
            while (_events.Any())
            {
                var e = _events.Last().Value;
                _events.RemoveAt(_events.Count - 1);
                yield return e.ProcessEvent(this);
            }
            RemoveRoutine();
        }

        private void RemoveRoutine()
        {
            _coroutineRunner.Stop(_coroutine);
            _coroutine = null;
        }
    }
}