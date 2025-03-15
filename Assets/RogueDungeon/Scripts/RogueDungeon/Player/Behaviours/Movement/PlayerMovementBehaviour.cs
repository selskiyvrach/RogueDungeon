using Common.Fsm;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerMovementBehaviour
    {
        private readonly Level _level;
        private readonly StateMachine _stateMachine;

        protected PlayerMovementBehaviour(StateMachine stateMachine, Level level)
        {
            _stateMachine = stateMachine;
            _level = level;
        }

        public void Initialize()
        {
            _level.LevelTraverser.LocalPosition = _level.StartingRoom.Coordinates;
            _level.LevelTraverser.Rotation = Vector2.up;
            _stateMachine.Initialize();
        }

        public void Tick(float deltaTime) => 
            _stateMachine.Tick(deltaTime);
    }
}