using Game.Libs.InGameResources;

namespace Game.Features.Combat.Domain
{
    public interface IStaminableCombatant
    {
        IResource Stamina { get; }
    }
}