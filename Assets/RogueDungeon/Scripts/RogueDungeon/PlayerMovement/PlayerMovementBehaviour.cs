using Common.Fsm;
using Common.Unity;
using RogueDungeon.Player;
using UnityEngine;
using StateMachineBehaviour = Common.Behaviours.StateMachineBehaviour;

namespace PlayerMovement
{
    public class PlayerMovementBehaviour : StateMachineBehaviour, ITwoDWorldObject, IPlayerMovementBehaviour
    {
        public TwoDWorldObject ObjectToMove { get; set; }
        
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

        protected PlayerMovementBehaviour(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enable()
        {
            ((TypeBasedTransitionStrategy)StateMachine.TransitionStrategy).SetStartState<TraversalIdleState>();
            base.Enable();
        }
    }
}