using Common.UnityUtils;
using RogueDungeon.Camera;

namespace RogueDungeon.Player
{
    public class PlayerCameraHandler
    {
        private readonly IGameCamera _gameCamera;
        private readonly IRootObject<UnityEngine.Camera> _cameraRoot;

        public PlayerCameraHandler(IGameCamera gameCamera, IRootObject<UnityEngine.Camera> cameraRoot)
        {
            _gameCamera = gameCamera;
            _cameraRoot = cameraRoot;
            _gameCamera.Follow = _cameraRoot.Transform;
        }
    }
}