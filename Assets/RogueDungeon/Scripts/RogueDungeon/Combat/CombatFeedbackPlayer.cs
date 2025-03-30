using RogueDungeon.Camera;

namespace RogueDungeon.Combat
{
    public class CombatFeedbackPlayer
    {
        private readonly IGameCamera _gameCamera;
        private readonly CombatFeedbackConfig _config;

        public CombatFeedbackPlayer(IGameCamera gameCamera, CombatFeedbackConfig config)
        {
            _gameCamera = gameCamera;
            _config = config;
        }

        public void OnHit() => 
            _gameCamera.KickPosition(_config.OnHitCameraPunch, _config.OnHitCameraPunchDuration);
    }
}