using Characters;

namespace RogueDungeon.Items.Model
{
    public interface IStaminaConsumingItemWielder
    {
        IResource Stamina { get; }
    }
}