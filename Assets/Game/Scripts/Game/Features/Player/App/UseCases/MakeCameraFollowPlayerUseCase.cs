using Game.Features.Player.Domain;
using Game.Libs.Time;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Features.Player.App.UseCases
{
    public class MakeCameraFollowPlayerUseCase : ITickable
    {
        private readonly Camera _camera;
        private readonly IPlayerSpawnedEventDispatcher _playerSpawnedEventDispatcher;
        private readonly IPlayerPovCameraPoint _playerPovCameraPoint;

        public MakeCameraFollowPlayerUseCase(Camera camera, IPlayerSpawnedEventDispatcher playerSpawnedEventDispatcher, IPlayerPovCameraPoint playerPovCameraPoint, IGameTime time)
        {
            _camera = camera;
            _playerSpawnedEventDispatcher = playerSpawnedEventDispatcher;
            _playerPovCameraPoint = playerPovCameraPoint;
            _playerSpawnedEventDispatcher.OnPlayerSpawned += _ => time.StartTicking(this, TickOrder.Camera);
        }

        public void Tick(float timeDelta)
        {
            _camera.transform.position = _playerPovCameraPoint.Position;
            _camera.transform.rotation = Quaternion.Euler(_playerPovCameraPoint.Rotation);
        }
    }
}