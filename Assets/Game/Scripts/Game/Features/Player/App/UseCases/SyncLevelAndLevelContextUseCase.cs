using System.Linq;
using Game.Features.Levels.Domain;
using Game.Features.Player.Domain.Movesets.Movement;
using UnityEngine;

namespace Game.Features.Player.App.UseCases
{
    public class SyncLevelAndLevelContextUseCase
    {
        private readonly LevelTraverserContext _levelContext;
        private readonly ILevelCreatedEventDispatcher _levelCreatedEventDispatcher;
        private Level _level;

        public SyncLevelAndLevelContextUseCase(LevelTraverserContext levelContext, ILevelCreatedEventDispatcher levelCreatedEventDispatcher)
        {
            _levelContext = levelContext;
            _levelCreatedEventDispatcher = levelCreatedEventDispatcher;
            _levelCreatedEventDispatcher.OnLevelCreated += HandleLevelCreated;
            _levelContext.OnRoomEntered += HandleRoomEntered;
            _levelContext.OnRoomExited += HandleRoomExited;
        }

        private void HandleLevelCreated(Level level)
        {
            _level = level;
            _levelContext.ExistingRooms = level.AllRooms.Select(n => n.Coordinates).ToHashSet();
        }

        private void HandleRoomEntered(Vector2Int obj) => 
            _level.GetRoom(obj).Enter();

        private void HandleRoomExited(Vector2Int obj) => 
            _level.GetRoom(obj).Exit();
    }
}