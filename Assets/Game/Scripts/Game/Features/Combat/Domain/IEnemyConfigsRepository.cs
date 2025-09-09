using Game.Features.Combat.Domain.Enemies;

namespace Game.Features.Combat.Domain
{
    public interface IEnemyConfigsRepository
    {
        public EnemyConfig Get(string id);
    }
}