using Common.Fsm;
using Common.MoveSets;

namespace RogueDungeon.Enemies
{
    public class EnemyLifeCycleMoveSetBehaviour : MoveSetBehaviour
    {
        public EnemylifeCycleMoveSetContext MoveSetContext { get; }

        public EnemyLifeCycleMoveSetBehaviour(StateMachine stateMachine, EnemylifeCycleMoveSetContext moveSetContext) : base(stateMachine) => 
            MoveSetContext = moveSetContext;
    }
}