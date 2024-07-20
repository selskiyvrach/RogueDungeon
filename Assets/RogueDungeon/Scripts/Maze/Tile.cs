using UnityEngine;

namespace RogueDungeon.Maze
{
    public interface IRoomInfo
    {
        bool IsVisited { get; set; }
        bool IsStartTile { get; set; }
    }

    public class Tile : IRoomInfo
    {
        public Vector2Int Coordinates { get; }
        public bool IsVisited { get; set; }
        public bool IsStartTile { get; set; }

        // distance to a start point
        // type (depends on:) 
            // distance to start point
            // adjacent tiles
            // current weights of tile types
            // distance to a closest tile of this type

        public Tile(Vector2Int coordinates) => 
            Coordinates = coordinates;
    }
}