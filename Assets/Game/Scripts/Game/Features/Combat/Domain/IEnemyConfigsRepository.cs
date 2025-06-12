using Game.Features.Combat.Domain.Enemies;

namespace Game.Features.Combat.Infrastructure
{
    public interface IEnemyConfigsRepository
    {
        public EnemyConfig Get(string id);
    }
}