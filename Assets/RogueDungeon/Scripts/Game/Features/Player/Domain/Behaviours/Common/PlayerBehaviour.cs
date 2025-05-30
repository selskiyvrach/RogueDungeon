using Game.Features.Levels;
using Game.Features.Levels.Domain;
using Libs.Fsm;
using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class PlayerBehaviour
    {
        private readonly Level _level;
        private readonly StateMachine _stateMachine;
        
        protected PlayerBehaviour(StateMachine stateMachine, Level level)
        {
            _stateMachine = stateMachine;
            _level = level;
        }

        public void Initialize()
        {
            _level.LevelTraverser.LocalPosition = _level.StartingRoom.Coordinates;
            _level.LevelTraverser.Rotation2D = Vector2.up;
            _stateMachine.Initialize();
        }

        public void Tick(float deltaTime) => 
            _stateMachine.Tick(deltaTime);
    }
}