using Common.Animations;

namespace RogueDungeon.Enemies.States
{
    public class EnemyBirthState : EnemyState
    {
        protected EnemyBirthState(EnemyStateConfig config, IAnimation animation) : base(config, animation)
        {
        }
    }
}