using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Enemies.MoveSet
{
    public abstract class EnemyMove : Move
    {
        protected EnemyMove(EnemyMoveConfig config, IAnimator animator) : base(config, animator)
        {
        }
    }
}