using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon.Maze
{
    public class Tile
    {
        public (string id, Position pos)[] Enemies { get; }
        public Vector2Int Coordinates { get; }

        public Tile(Vector2Int coordinates, (string id, Position pos)[] enemies = null)
        {
            Coordinates = coordinates;
            Enemies = enemies;
        }

        public void OnEntered(Game game)
        {
            if (Enemies == null) 
                return;
            
            foreach (var enemy in Enemies) 
                game.CreateCharacter(enemy.id, enemy.pos);
        }
    }
}