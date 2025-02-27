using Common.Fsm;
using Common.MoveSets;

namespace RogueDungeon.Enemies.Attacks
{
    public class AttackBehaviour : MoveSetBehaviour
    {
        public bool IsAttacking { get; }

        public AttackBehaviour(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public void StartAttack()
        {
        } 
    }
}