using UnityEngine;

namespace RogueDungeon.Maze
{
    public class Tile
    {
        public Vector2Int Coordinates { get; }

        public bool IsStartTile { get; set; }

        // distance to a start point
        // type (depends on:) 
            // distance to start point
            // adjacent tiles
            // current weights of tile types
            // distance to a closest tile of this type
        public bool Visited { get; private set; }

        public Tile(Vector2Int coordinates) => 
            Coordinates = coordinates;

        public virtual void OnEntered(Game game)
        {
            if(Visited)
                return;
            Visited = true;
            if(IsStartTile)
                return;
            if(Random.Range(0, 10) < 7)
                return;
            var enemiesCount = Random.Range(0, 10) switch
            {
                < 4 => 1,
                < 7 => 2,
                _ => 3,
            };
            for (var i = 0; i < enemiesCount; i++) 
                game.CreateCharacter("test-skeleton-swordsman");
        }
    }
}