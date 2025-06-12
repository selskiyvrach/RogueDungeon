using Game.Features.Levels.Domain;
using Game.Features.Player.Domain.Movesets.Movement;

namespace Game.Features.Combat.App
{
    public class TryStartCombatOnRoomEnterUseCase
    {
        private readonly Level _level;
        private readonly Domain.Combat _combat;
        private readonly ILevelTraverser _levelTraverser;
        private readonly Player.Domain.Player _player;

        public TryStartCombatOnRoomEnterUseCase(Level level, Domain.Combat combat, ILevelTraverser levelTraverser, Player.Domain.Player player)
        {
            _level = level;
            _combat = combat;
            _levelTraverser = levelTraverser;
            _player = player;
            _level.OnRoomEntered += HandleRoomEntered;
            _combat.OnFinished += () => _player.IsInCombat = false;
        }

        private void HandleRoomEntered(Room obj)
        {
            _combat.Initiate(obj.CombatId, obj.Coordinates, _levelTraverser.GridRotation);
            _player.IsInCombat = true;
        }
    }
}