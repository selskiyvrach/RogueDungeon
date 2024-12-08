using Common.Camera;
using Common.GameObjectMarkers;

namespace RogueDungeon.Player
{
    public class PlayerCameraHandler
    {
        private readonly IGameCamera _gameCamera;
        private readonly CameraParentObject _cameraParent;

        public PlayerCameraHandler(IGameCamera gameCamera, CameraParentObject cameraParent)
        {
            _gameCamera = gameCamera;
            _cameraParent = cameraParent;
            _gameCamera.Follow = _cameraParent.transform;
        }
    }
}