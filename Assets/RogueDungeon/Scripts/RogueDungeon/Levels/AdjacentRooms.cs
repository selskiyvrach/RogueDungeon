using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Levels
{
    public class AdjacentRooms
    {
        private readonly IRoom _thisRoom;
        public IRoom Top {get;}
        public IRoom Bottom {get;}
        public IRoom Left {get;}
        public IRoom Right {get;}

        public bool HasTop => Top != null; 
        public bool HasBottom => Bottom != null; 
        public bool HasLeft => Left != null; 
        public bool HasRight => Right != null;

        public AdjacentRooms(IRoom thisRoom, IEnumerable<IRoom> adjacentRooms)
        {
            _thisRoom = thisRoom;
            foreach (var room in adjacentRooms)
            {
                if(room.Coordinates - _thisRoom.Coordinates == Vector2Int.up && Top == null)
                    Top = room;
                else if(room.Coordinates - _thisRoom.Coordinates == Vector2Int.down && Bottom == null)
                    Bottom = room;
                else if (room.Coordinates - _thisRoom.Coordinates == Vector2Int.left && Left == null)
                    Left = room;
                else if (room.Coordinates - _thisRoom.Coordinates == Vector2Int.right && Right == null)
                    Right = room;
                else
                    throw new Exception("Invalid room coordinates");
            }
        }

        public bool HasAdjacentRoom(Adjacency adjacency) =>
            adjacency switch
            {
                Adjacency.Top => HasTop,
                Adjacency.Bottom => HasBottom,
                Adjacency.Left => HasLeft,
                Adjacency.Right => HasRight,
                _ => throw new ArgumentOutOfRangeException(nameof(adjacency), adjacency, null)
            };

        public IRoom Get(Adjacency adjacency) =>
            adjacency switch
            {
                Adjacency.Top => Top,
                Adjacency.Bottom => Bottom,
                Adjacency.Left => Left,
                Adjacency.Right => Right,
                _ => throw new ArgumentOutOfRangeException(nameof(adjacency), adjacency, null)
            };
    }
}