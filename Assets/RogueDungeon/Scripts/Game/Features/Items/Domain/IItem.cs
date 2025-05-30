using Game.Features.Items.Domain.Configs;

namespace Game.Features.Items.Domain
{
    public interface IItem
    {
        int InstanceId { get; }
        ItemConfig Config { get; }
    }
}