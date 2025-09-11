namespace Game.Features.Inventory.Domain
{
    public interface ILootConfigsRepository
    {
        ILootConfig GetLootConfig(string lootId);
    }
}