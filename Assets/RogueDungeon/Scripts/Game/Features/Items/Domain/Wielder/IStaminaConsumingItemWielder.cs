using Game.Libs.InGameResources;

namespace Game.Features.Items.Domain.Wielder
{
    public interface IStaminaConsumingItemWielder
    {
        IResource Stamina { get; }
    }
}