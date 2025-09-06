using System;
using System.Linq;
using Game.Features.Levels.Domain;
using Game.Features.Player.Domain.Movesets.Movement;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class SyncLevelAndLevelContextUseCase : IInitializable, IDisposable
    {
        private readonly LevelTraverserContext _levelContext;
        private readonly Level _level;

        public SyncLevelAndLevelContextUseCase(LevelTraverserContext levelContext, Level level)
        {
            _levelContext = levelContext;
            _levelContext.OnRoomEntered += HandleRoomEntered;
            _levelContext.OnRoomExited += HandleRoomExited;
            _level = level;
        }

        public void Initialize() => 
            _levelContext.ExistingRooms = _level.AllRooms.Select(n => n.Coordinates).ToHashSet();

        public void Dispose()
        {
            _levelContext.OnRoomEntered -= HandleRoomEntered;
            _levelContext.OnRoomExited -= HandleRoomExited;
        }

        private void HandleRoomEntered(Vector2Int obj) => 
            _level.GetRoom(obj).Enter();

        private void HandleRoomExited(Vector2Int obj) => 
            _level.GetRoom(obj).Exit();
    }
}