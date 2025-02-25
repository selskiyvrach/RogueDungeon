using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Enemies
{
    public class EnemyMove : Move
    {
        private readonly EnemyMoveConfig _config;

        protected EnemyMove(EnemyMoveConfig config, IAnimator animator) : base(config, animator) => 
            _config = config;
    }
}