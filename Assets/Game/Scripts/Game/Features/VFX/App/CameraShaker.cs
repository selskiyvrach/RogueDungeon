using DG.Tweening;
using UnityEngine;

namespace Game.Features.VFX.App
{
    public class CameraShaker : ICameraShaker
    {
        private readonly ICameraShakerConfig _config;
        private readonly Camera _camera;
        private Tweener _currentShake;

        public CameraShaker(Camera camera, ICameraShakerConfig config)
        {
            _camera = camera;
            _config = config;
        }

        public void DoShake(ShakeIntensity intensity)
        {
            _currentShake?.Kill(complete: true);
            _currentShake = _camera.DOShakePosition(_config.GetShakeDuration(intensity), _config.GetShakeStrength(intensity));
        }
    }
}