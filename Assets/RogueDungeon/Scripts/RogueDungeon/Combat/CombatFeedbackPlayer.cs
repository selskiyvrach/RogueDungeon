using System;
using Camera;
using Enemies;
using UnityEngine;

namespace Combat
{
    public class CombatFeedbackPlayer
    {
        private readonly IGameCamera _gameCamera;
        private readonly CombatFeedbackConfig _config;
        private float _lastTime = float.NegativeInfinity;
        private HitSeverity _lastSeverity = HitSeverity.Regular;

        public CombatFeedbackPlayer(IGameCamera gameCamera, CombatFeedbackConfig config)
        {
            _gameCamera = gameCamera;
            _config = config;
        }

        public void OnHit(HitSeverity severity)
        {
            switch (severity)
            {
                case HitSeverity.Regular:
                    if(_lastSeverity == HitSeverity.Critical && Time.time - _lastTime < _config.OnCriticalHitCameraShakeDuration * .75)
                        break;
                    _gameCamera.KickPosition(Vector3.one * _config.OnHitCameraPunch, _config.OnHitCameraPunchDuration);
                    break;
                case HitSeverity.Critical:
                    _gameCamera.KickPosition(Vector3.one * _config.OnCriticalHitCameraPunch, _config.OnCriticalHitCameraShakeDuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }

            _lastTime = Time.time;
            _lastSeverity = severity;
        }
    }
}