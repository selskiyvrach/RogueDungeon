namespace Game.Features.Inventory.Infrastructure.View
{
    public interface ICurrentRoomLootAreaProvider
    {
        ItemContainer GetCurrentRoomLootArea();
    }
}