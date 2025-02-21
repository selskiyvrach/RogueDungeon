using Common.Fsm;
using RogueDungeon.Input;
using RogueDungeon.Levels;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class TraversalIdleState : ITypeBasedTransitionableState
    {
        private readonly ILevelTraverser _levelTraverser;
        private readonly RotateState _rotateState;
        private readonly MoveForwardState _moveForwardState;
        private readonly IPlayerInput _input;
        private readonly IRoom _currentRoom;

        public TraversalIdleState(IPlayerInput input) => 
            _input = input;

        public void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if (_input.HasInput(InputKey.MoveForward) && _currentRoom.AdjacentRooms.HasAdjacentRoom(_levelTraverser.Direction.ToAdjacency()))
            {
                _input.ConsumeInput(InputKey.MoveForward);
                stateChanger.ChangeState<MoveForwardState>();
            }
            else if (_input.HasInput(InputKey.TurnLeft))
            {
                _input.ConsumeInput(InputKey.TurnLeft);
                _rotateState.Rotation = Rotation.Left;
                stateChanger.ChangeState<RotateState>();
            }
            else if (_input.HasInput(InputKey.TurnRight))
            {
                _input.ConsumeInput(InputKey.TurnRight);
                _rotateState.Rotation = Rotation.Right;
                stateChanger.ChangeState<RotateState>();
            }
            else if (_input.HasInput(InputKey.TurnAround))
            {
                _input.ConsumeInput(InputKey.TurnAround);
                _rotateState.Rotation = Rotation.Around;
                stateChanger.ChangeState<RotateState>();
            }
        }
    }
}