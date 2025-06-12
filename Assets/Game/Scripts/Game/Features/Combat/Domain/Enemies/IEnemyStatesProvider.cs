namespace Game.Features.Combat.Domain.Enemies
{
    public interface IEnemyStatesProvider
    {
        EnemyMove GetState(string name);
    }
}