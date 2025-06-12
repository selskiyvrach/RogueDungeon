namespace Game.Features.Combat.Domain
{
    public interface ICombatConfigsRepository
    {
        ICombatConfig Get(string id);
    }
}