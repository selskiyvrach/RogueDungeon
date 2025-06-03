using Game.Features.Enemies.Domain.Moves;

namespace Game.Features.Enemies.Domain
{
    public interface IEnemyStatesProvider
    {
        EnemyMove GetState(string name);
    }
}