using System.Linq;
using Common.Fsm;
using Common.Unity;
using RogueDungeon.Input;
using RogueDungeon.Levels;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class TraversalIdleState : ITypeBasedTransitionableState
    {
        private readonly ILevelTraverser _levelTraverser;
        private readonly IPlayerInput _input;
        private readonly Level _level;

        public TraversalIdleState(IPlayerInput input, ILevelTraverser levelTraverser, Level level)
        {
            _input = input;
            _levelTraverser = levelTraverser;
            _level = level;
        }

        public void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if (_input.HasInput(InputKey.MoveForward) && 
                _level.Rooms.First(n => n.Coordinates == _levelTraverser.Position.Round()).AdjacentRooms.
                    HasAdjacentRoom(_levelTraverser.Direction.Round()))
            {
                stateChanger.ChangeState<MoveForwardState>();
            }
            else if (_input.HasInput(InputKey.TurnLeft))
            {
                stateChanger.ChangeState<RotateState>();
            }
            else if (_input.HasInput(InputKey.TurnRight))
            {
                stateChanger.ChangeState<RotateState>();
            }
            else if (_input.HasInput(InputKey.TurnAround))
            {
                stateChanger.ChangeState<RotateState>();
            }
        }
    }
}