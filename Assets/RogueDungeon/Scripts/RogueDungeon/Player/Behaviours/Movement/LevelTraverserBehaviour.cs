using Common.Fsm;
using Common.Unity;
using RogueDungeon.Levels;
using UnityEngine;
using StateMachineBehaviour = Common.Behaviours.StateMachineBehaviour;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class LevelTraverserBehaviour : StateMachineBehaviour, ITwoDWorldObject
    {
        public ITwoDWorldObject LevelTraverser { get; set; }

        public Vector2 LocalPosition
        {
            get => LevelTraverser.LocalPosition; 
            set => LevelTraverser.LocalPosition = value; 
        }

        public Vector2 Rotation
        {
            get => LevelTraverser.Rotation; 
            set => LevelTraverser.Rotation = value; 
        }

        protected LevelTraverserBehaviour(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enable()
        {
            ((TypeBasedTransitionStrategy)StateMachine.TransitionStrategy).SetStartState<TraversalIdleState>();
            base.Enable();
        }
    }
}