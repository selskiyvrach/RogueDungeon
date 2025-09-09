using System;
using Game.Features.VFX.App;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.VFX.Infrastructure
{
    public class CameraShakerConfig : ScriptableObject, ICameraShakerConfig
    {
        [SerializeField, HorizontalGroup("1")] private float _mildDuration = .1f;
        [SerializeField, HorizontalGroup("1")] private float _mildStrength = .1f;
        [SerializeField, HorizontalGroup("2")] private float _strongDuration = .1f;
        [SerializeField, HorizontalGroup("2")] private float _strongStrength = .1f;
        [SerializeField, HorizontalGroup("3")] private float _extremeDuration = .1f;
        [SerializeField, HorizontalGroup("3")] private float _extremeStrength = .1f;
        
        public float GetShakeDuration(ShakeIntensity intensity) =>
            intensity switch
            {
                ShakeIntensity.Mild => _mildDuration,
                ShakeIntensity.Strong => _strongDuration,
                ShakeIntensity.Extreme => _extremeDuration,
                _ => throw new ArgumentOutOfRangeException(nameof(intensity), intensity, null)
            };

        public float GetShakeStrength(ShakeIntensity intensity) =>
            intensity switch
            {
                ShakeIntensity.Mild => _mildStrength,
                ShakeIntensity.Strong => _strongStrength,
                ShakeIntensity.Extreme => _extremeStrength,
                _ => throw new ArgumentOutOfRangeException(nameof(intensity), intensity, null)
            };
    }
}