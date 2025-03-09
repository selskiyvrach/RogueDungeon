using Common.Fsm;
using Common.Unity;
using RogueDungeon.Levels;
using RogueDungeon.Player;
using UnityEngine;

namespace PlayerMovement
{
    public class PlayerMovement : ITwoDWorldObject, IPlayerMovementBehaviour
    {
        private readonly StateMachine _stateMachine;
        
        // this is a hack to decouple player->behaviour->player dependency in creation time
        public ITwoDWorldObject ObjectToMove { get; set; }
        
        public Vector2 LocalPosition
        {
            get => ObjectToMove.LocalPosition; 
            set => ObjectToMove.LocalPosition = value; 
        }

        public Vector2 Rotation
        {
            get => ObjectToMove.Rotation; 
            set => ObjectToMove.Rotation = value; 
        }

        protected PlayerMovement(StateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Initialize() => 
            _stateMachine.Initialize();

        public void Tick(float deltaTime) => 
            _stateMachine.Tick(deltaTime);
    }
}