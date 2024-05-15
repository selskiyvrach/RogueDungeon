using RogueDungeon.Characters;
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
            if(Random.Range(0, 10) < 5)
                return;
            switch (Random.Range(0, 8))
            {
                case 7: 
                    game.CreateCharacter("zombie-crusher");
                    break;
                case 6: 
                    game.CreateCharacter("zombie-warrior", Position.Frontline);
                    game.CreateCharacter("zombie");
                    game.CreateCharacter("zombie");
                    break;
                case 5:
                    game.CreateCharacter("zombie-warrior", Position.Frontline);
                    break;
                case 4:
                    game.CreateCharacter("zombie");
                    game.CreateCharacter("zombie");
                    game.CreateCharacter("zombie");
                    break;
                case 3:
                    game.CreateCharacter("zombie");
                    game.CreateCharacter("zombie");
                    break;
                default:
                    game.CreateCharacter("zombie");
                    break;
            }
        }
    }
}