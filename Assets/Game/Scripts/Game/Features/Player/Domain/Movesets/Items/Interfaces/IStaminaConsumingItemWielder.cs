using Game.Libs.InGameResources;

namespace Game.Features.Player.Domain.Movesets.Items.Interfaces
{
    public interface IStaminaConsumingItemWielder
    {
        IResource Stamina { get; }
    }
}