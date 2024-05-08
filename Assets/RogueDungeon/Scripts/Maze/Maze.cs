using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon.Maze
{
    public class Maze
    {
        private readonly Dictionary<Vector2Int, Tile> _tiles;
        private readonly Game _game;
        
        private Vector2Int _currentCoordinates;

        public Maze(Game game, IEnumerable<Tile> tiles)
        {
            _game = game;
            _tiles = tiles.ToDictionary(n => n.Coordinates, n => n);
            MoveTo(Vector2Int.zero);
        }
             

        public void MoveTo(Vector2Int coords)
        {
            _currentCoordinates = coords;
            _tiles[coords].OnEntered(_game);
        }
    }

    public class Tile
    {
        private (string id, Position pos)[] _enemies;
        
        public Vector2Int Coordinates { get; }

        public void OnEntered(Game game)
        {
            foreach (var enemy in _enemies) 
                game.CreateCharacter(enemy.id, enemy.pos);
        }
    }
}