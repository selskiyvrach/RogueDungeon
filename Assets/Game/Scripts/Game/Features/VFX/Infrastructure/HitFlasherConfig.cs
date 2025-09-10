using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Features.VFX.Infrastructure
{
    public class HitFlasherConfig : ScriptableObject, IHitFlasherConfig
    {
        [SerializeField] private float _frontSize;
        [SerializeField] private float _backSize;
        [SerializeField] private float _backTravelDistance;
        [SerializeField] private float _frontTravelDistance;
        [SerializeField] private Vector3 _frontRightPos;
        [SerializeField] private Vector3 _frontLeftPos;
        [SerializeField] private Vector3 _backRightPos;
        [SerializeField] private Vector3 _backLeftPos;
        
        [field: SerializeField] public float FlashInDuration { get; private set; }
        [field: SerializeField] public float FlashOutDuration { get; private set; }

        public float GetTravelDistance(HitFlashPosition position) =>
            position switch
            {
                HitFlashPosition.FrontRight or HitFlashPosition.FrontLeft => _frontTravelDistance,
                HitFlashPosition.BackRight or HitFlashPosition.BackLeft => _backTravelDistance,
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
            };

        public Vector3 GetFlashLocalPosition(HitFlashPosition pos) =>
            pos switch
            {
                HitFlashPosition.FrontRight => _frontRightPos,
                HitFlashPosition.FrontLeft => _frontLeftPos,
                HitFlashPosition.BackRight => _backRightPos,
                HitFlashPosition.BackLeft => _backLeftPos,
                _ => throw new ArgumentOutOfRangeException(nameof(pos), pos, null)
            };

        public float GetFlashSize(HitFlashPosition pos) =>
            pos switch
            {
                HitFlashPosition.FrontRight or HitFlashPosition.FrontLeft => _frontSize,
                HitFlashPosition.BackRight or HitFlashPosition.BackLeft => _backSize,
                _ => throw new ArgumentOutOfRangeException(nameof(pos), pos, null)
            };
    }
}