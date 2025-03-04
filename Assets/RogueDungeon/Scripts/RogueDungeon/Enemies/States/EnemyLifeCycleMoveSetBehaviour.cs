using Common.Fsm;
using Common.MoveSets;

namespace RogueDungeon.Enemies.States
{
    public class EnemyLifeCycleMoveSetBehaviour : MoveSetBehaviour
    {
        public EnemylifeCycleMoveSetContext MoveSetContext { get; }

        public EnemyLifeCycleMoveSetBehaviour(StateMachine stateMachine, EnemylifeCycleMoveSetContext moveSetContext) : base(stateMachine) => 
            MoveSetContext = moveSetContext;
    }
}