namespace Game.Features.Loot.Domain
{
    public interface ILootConfigsRepository
    {
        ILootConfig GetLootConfig(string lootId);
    }
}