using System;
using Game.Features.Combat.Domain;
using Game.Features.Levels.Domain;
using Game.Features.Player.Domain.Movesets.Movement;
using Libs.Utils.DotNet;
using Zenject;

namespace Game.Features.Combat.App.UseCases
{
    public class TryStartCombatOnRoomEnterUseCase : IInitializable, IDisposable
    {
        private readonly Level _level;
        private readonly ICombat _combat;
        private readonly ILevelTraverser _levelTraverser;
        private readonly Player.Domain.Player _player;

        public TryStartCombatOnRoomEnterUseCase(Level level, ICombat combat, ILevelTraverser levelTraverser, Player.Domain.Player player)
        {
            _level = level;
            _combat = combat;
            _levelTraverser = levelTraverser;
            _player = player; 
        }

        public void Initialize()
        {
            _level.OnRoomEntered += OnOnRoomEntered;
            _combat.OnStarted += OnCombatStarted;
            _combat.OnFinished += OnCombatFinished;
        }

        public void Dispose()
        {
            _level.OnRoomEntered -= OnOnRoomEntered;
            _combat.OnStarted -= OnCombatStarted;
            _combat.OnFinished -= OnCombatFinished;
        }

        private void OnOnRoomEntered(Room room)
        {
            if(!room.CombatId.IsNullOrEmpty())
                _combat.Initiate(room.CombatId, room.Coordinates, _levelTraverser.GridRotation);
        }

        private void OnCombatStarted() => 
            _player.IsInCombat = true;

        private void OnCombatFinished() => 
            _player.IsInCombat = false;
    }
}