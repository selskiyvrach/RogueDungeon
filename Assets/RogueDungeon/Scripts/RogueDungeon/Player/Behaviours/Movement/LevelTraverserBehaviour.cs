using System;
using Common.Behaviours;
using Common.Unity;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public interface ILevelTraverser
    {
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
    }

    public abstract class TraversalAction
    {
        private float _timePassed;
        
        protected readonly LevelTraverserConfig Config;
        protected abstract float Duration { get; }
        protected readonly ILevelTraverser LevelTraverser;
        public bool IsFinished => _timePassed >= Duration;

        protected TraversalAction(ILevelTraverser levelTraverser, LevelTraverserConfig config)
        {
            LevelTraverser = levelTraverser;
            Config = config;
        }

        public virtual void Start()
        {
            // fix position and rotation
            LevelTraverser.Direction = LevelTraverser.Direction.Round();
            LevelTraverser.Position = LevelTraverser.Position.Round();
            
            _timePassed = 0;
            SetValueNormalized(0);
        }

        public void Tick(float deltaTime)
        {
            if(!IsFinished)
                SetValueNormalized((_timePassed += deltaTime) / Duration);
        }

        protected abstract void SetValueNormalized(float value);

        protected Vector2 GetRealPosition(Vector2 tile) => 
            tile + LevelTraverser.Direction * - Config.PositionOffsetFromTileCenter;
    }
    
    public class RotateAction : TraversalAction
    {
        private Vector2Int _from;
        private Vector2Int _to;
        protected override float Duration => Config.RotationDuration;
        public Rotation Rotation { get; set; }

        public RotateAction(ILevelTraverser levelTraverser, LevelTraverserConfig config) : base(levelTraverser, config)
        {
        }

        public override void Start()
        {
            base.Start();
            _from = LevelTraverser.Direction.Round();
            _to = ((Vector2)_from).Rotate(Rotation switch {
                Rotation.Left => 90,
                Rotation.Right => -90,
                Rotation.Around => 180,
                _ => throw new ArgumentOutOfRangeException()
            }).Round();
        }

        protected override void SetValueNormalized(float value)
        {
            var angle = Vector2.SignedAngle(_from, _to) * value;
            LevelTraverser.Direction = LevelTraverser.Direction.Rotate(angle);
            LevelTraverser.Position = GetRealPosition(LevelTraverser.Position.Round());
        }
    }

    public class MoveAction : TraversalAction
    {
        protected override float Duration => Config.MoveDuration;
        public Vector2Int From { get; set; }
        public Vector2Int To { get; set; }


        public MoveAction(ILevelTraverser levelTraverser, LevelTraverserConfig config) : base(levelTraverser, config)
        {
        }

        protected override void SetValueNormalized(float value) => 
            LevelTraverser.Position = Vector3.Lerp(GetRealPosition(From), GetRealPosition(To), value);
    }

    public class LevelTraverserBehaviour : TickableBehaviour
    {
        private readonly RotateAction _rotateAction;
        private readonly MoveAction _moveAction;
        
        private TraversalAction _currentAction;
        
        private readonly Level _level;
        private IRoom _currentRoom;

        public LevelTraverserBehaviour(Level level, RotateAction rotateAction, MoveAction moveAction)
        {
            _level = level;
            _rotateAction = rotateAction;
            _moveAction = moveAction;
        }

        public override void Enable()
        {
            base.Enable();
            MoveToRoom(_level.StartingRoom);     
        }

        protected override void Tick(float timeDelta)
        {
            if(_currentAction == null)
                return;
            
            _currentAction.Tick(timeDelta);
            if(_currentAction.IsFinished)
                _currentAction = null;
        }

        public bool CanMove(Adjacency adjacency) => 
            _currentRoom.AdjacentRooms.HasAdjacentRoom(adjacency);

        public void Move(Adjacency adjacency) => 
            MoveToRoom(_currentRoom.AdjacentRooms.Get(adjacency));

        public void Turn(Rotation rotation)
        {
            if(_currentAction != null)
                throw new Exception("Another action is currently being executed.");
            _rotateAction.Rotation = rotation;
            _currentAction = _rotateAction;
            _currentAction.Start();
        }

        private void EnterRoom(IRoom room)
        {
            _currentRoom = room;
            _currentRoom.Enter();
        }

        private void MoveToRoom(IRoom room)
        {
            if(_currentAction != null)
                throw new Exception("Another action is currently being executed.");
            _moveAction.From = _currentRoom.Coordinates;
            _moveAction.To = room.Coordinates;
            _currentAction = _moveAction;
            _currentAction.Start();
        }
    }
    
    public enum Rotation
    {
        None,
        Left,
        Right,
        Around
    }
}