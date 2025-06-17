using Game.Features.Levels.Domain;
using Game.Features.Player.Domain.Movesets.Movement;

namespace Game.Features.Combat.App
{
    public class TryStartCombatOnRoomEnterUseCase
    {
        public TryStartCombatOnRoomEnterUseCase(Level level, Domain.Combat combat, ILevelTraverser levelTraverser, Player.Domain.Player player)
        {
            level.OnRoomEntered += obj => combat.Initiate(obj.CombatId, obj.Coordinates, levelTraverser.GridRotation);
            combat.OnStarted += () => player.IsInCombat = true;
            combat.OnFinished += () => player.IsInCombat = false;
        }
    }
}