using Game.Features.Levels.Domain;
using Game.Features.Player.Domain.Movesets.Movement;

namespace Game.Features.Combat.App
{
    public class TryStartCombatOnRoomEnterUseCase
    {
        private readonly Level _level;
        private readonly Domain.Combat _combat;
        private readonly ILevelTraverser _levelTraverser;

        public TryStartCombatOnRoomEnterUseCase(Level level, Domain.Combat combat, ILevelTraverser levelTraverser)
        {
            _level = level;
            _combat = combat;
            _levelTraverser = levelTraverser;
            _level.OnRoomEntered += HandleRoomEntered;
        }

        private void HandleRoomEntered(Room obj) => 
            _combat.Initiate(obj.CombatId, obj.Coordinates, _levelTraverser.GridRotation);
    }
}