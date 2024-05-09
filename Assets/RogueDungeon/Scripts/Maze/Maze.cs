using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Characters;
using RogueDungeon.Input;
using UnityEngine;

namespace RogueDungeon.Maze
{
    public class Maze
    {
        private readonly Dictionary<Vector2Int, Tile> _tiles;
        private readonly Game _game;
        
        private Vector2Int _currentCoordinates;
        private Vector2Int _facingDirection;
        
        private readonly int _moveDuration = 30;
        private int _movedForFrames;
        private bool _isMoving;

        public Vector2 WorldPosition { get; private set; }

        public Maze(Game game, IEnumerable<Tile> tiles)
        {
            _game = game;
            _tiles = tiles.ToDictionary(n => n.Coordinates, n => n);
            _facingDirection = Vector2Int.up;
            MoveTo(Vector2Int.zero);
        }

        public void Tick()
        {
            if (_isMoving)
            {
                var distCovered = (float)++_movedForFrames / _moveDuration;
                WorldPosition = Vector2.Lerp(_currentCoordinates, _currentCoordinates + _facingDirection, distCovered);
                if (distCovered < 1)
                    return;
                _isMoving = false;
                MoveTo(_currentCoordinates + _facingDirection);
            }

            if(!Input.Input.GetUnit(Action.MoveForward).Held)
                return;
            if(!_tiles.ContainsKey(_currentCoordinates + _facingDirection))
                return;
            _isMoving = true;
            _movedForFrames = 0;
        }

        private void MoveTo(Vector2Int coords)
        {
            _currentCoordinates = coords;
            _tiles[coords].OnEntered(_game);
        }
    }

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