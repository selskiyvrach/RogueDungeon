using Common.Fsm;
using RogueDungeon.Levels;
using UnityEngine;
using StateMachineBehaviour = Common.Behaviours.StateMachineBehaviour;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class LevelTraverserBehaviour : StateMachineBehaviour, ILevelTraverser
    {
        public ILevelTraverser LevelTraverser { get; set; }
        public Vector2 Position
        {
            get => LevelTraverser.Position;
            set => LevelTraverser.Position = value;
        }

        public Vector2 Direction
        {
            get => LevelTraverser.Direction;
            set => LevelTraverser.Direction = value;
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