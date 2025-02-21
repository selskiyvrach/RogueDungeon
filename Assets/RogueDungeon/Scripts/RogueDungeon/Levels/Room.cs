using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RogueDungeon.Levels
{
    public class Room : IRoom
    {
        private readonly SortedList<RoomEventPriority, IRoomEvent> _events = new();
        private IRoomEvent _eventBeingHandled;

        public Vector2Int Coordinates { get; }
        public AdjacentRooms AdjacentRooms { get; }
        
        public void Enter() => 
            ProcessNextEvent();

        public void AddEvent(IRoomEvent roomEvent) => 
            _events.Add(roomEvent.Priority, roomEvent);

        public void OnAfterEventHandled(IRoomEvent roomEvent)
        {
            if(_eventBeingHandled == null) 
                throw new Exception("Event is not being handled");
            if(roomEvent == null)
                throw new Exception("Event is null");
            if(roomEvent != _eventBeingHandled)
                throw new Exception("Wrong event");
            _eventBeingHandled = null;
            ProcessNextEvent();
        }

        private void ProcessNextEvent()
        {
            if (_eventBeingHandled != null)
                throw new Exception("Another event is already being handled");
            _eventBeingHandled = _events.Last().Value;
            _events.RemoveAt(_events.Count - 1);
            _eventBeingHandled.Trigger(this);
        }
    }
}