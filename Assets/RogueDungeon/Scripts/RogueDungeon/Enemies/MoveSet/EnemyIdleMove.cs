using Common.Animations;

namespace RogueDungeon.Enemies.MoveSet
{
    
    // enemy behaviour -> moveset
    // leaving all other questions for later
    
    
    
    public class EnemyIdleMove : EnemyMove
    {
        private readonly Enemy _enemy;
        private readonly EnemyMoveConfig _config;

        public EnemyIdleMove(EnemyMoveConfig config, IAnimator animator, Enemy enemy) : base(config, animator)
        {
            _config = config;
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            // _context.EnemySpecificMoveSet.Enable();
        }

        public override void Exit()
        {
            base.Exit();
            // _context.EnemySpecificMoveSet.Disable();
        }
    }
}