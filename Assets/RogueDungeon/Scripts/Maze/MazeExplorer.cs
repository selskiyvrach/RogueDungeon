using RogueDungeon.Time;
using UnityEngine;

namespace RogueDungeon.Maze
{
    public class MazeExplorer
    {
        private readonly Maze _maze;
        private readonly ITime _time;
        private readonly IRoomEnteredListener _roomEnteredListener;
        private readonly IIsOnCrossroadListener _isOnCrossroadListener;

        private Vector2Int _position;
        private Vector2Int _direction;
        private Vector2Int _targetDirection;
        
        private readonly int _moveDuration = 15;
        private readonly int _rotationDuration = 15;
        private int _movedForFrames;
        private int _rotatedForFrames;
        private bool _isMoving;
        private bool _isRotating;
        // private readonly IInput _input;

        public Transform MazeCursor { get; }
        public bool IsOnCrossroad { get; private set; }

        public MazeExplorer(ITime time, IRoomEnteredListener roomEnteredListener, IIsOnCrossroadListener isOnCrossroadListener, Maze maze, Transform mazeCursor)// IInput input)
        {
            _time = time;
            _roomEnteredListener = roomEnteredListener;
            _isOnCrossroadListener = isOnCrossroadListener;
            _maze = maze;
            // _input = input;
            MazeCursor = mazeCursor;
            _direction = Vector2Int.up;
            _time.EachTick(Tick, TickOrders.MAZE_EXPLORER);
            MoveTo(_maze.StartingPoint);
        }

        public void Tick()
        {
            if (_isMoving)
            {
                var distCovered = (float)++_movedForFrames / _moveDuration;
                var localPos = Vector2.Lerp(_position, _position + _direction, distCovered);
                MazeCursor.position = new Vector3(localPos.x, 0, localPos.y);
                if (distCovered < 1)
                    return;
                _isMoving = false;
                MoveTo(_position + _direction);
            }
            
            if (_isRotating)
            {
                var progress = (float)++_rotatedForFrames / _rotationDuration;
                var worldDir = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y));
                var targetWorldDir = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0, _targetDirection.y));
                MazeCursor.rotation = Quaternion.Lerp(worldDir, targetWorldDir, progress);
                if (progress < 1)
                    return;
                _isRotating = false;
                _direction = _targetDirection;
            }

            // if (IsOnCrossroad)
            // {
            //     if (_input.GetUnit(Action.ChooseForward).Down)
            //         MoveForward();
            //
            //     else if (_input.GetUnit(Action.ChooseBack).Down)
            //         RotateTo(_maze.TurnAround(_direction));
            //
            //     else if (_input.GetUnit(Action.ChooseLeft).Down)
            //         RotateTo(_maze.TurnCounterclockwise(_direction));
            //
            //     else if (_input.GetUnit(Action.ChooseRight).Down) 
            //         RotateTo(_maze.TurnClockwise(_direction));
            //
            //     return;
            // }
            //
            // if (_input.GetUnit(Action.MoveForward).Down)
            // {
            //     MoveForward();
            //     return;
            // }
            //
            // if (_input.GetUnit(Action.TurnAround).Down) 
            //     RotateTo(_maze.TurnAround(_direction));
        }

        private void RotateTo(Vector2Int targetDirection)
        {
            _isRotating = true;
            _rotatedForFrames = 0;
            _targetDirection = targetDirection;
        }

        private void MoveForward()
        {
            if (!_maze.HasTile(_position + _direction))
                return;
            _isMoving = true;
            _movedForFrames = 0;
        }

        private void MoveTo(Vector2Int coords)
        {
            _position = coords;
            _roomEnteredListener.OnRoomEntered(_maze.GetTile(coords));
            IsOnCrossroad = _maze.IsCrossroad(coords);
            MazeCursor.position = new Vector3(_position.x, 0, _position.y);
            _isOnCrossroadListener.SetIsOnCrossroad(IsOnCrossroad);
        }
    }

    public interface IIsOnCrossroadListener
    {
        void SetIsOnCrossroad(bool isOnCrossroad);
    }

    public interface IRoomEnteredListener
    {
        void OnRoomEntered(Tile tile);
    }
}