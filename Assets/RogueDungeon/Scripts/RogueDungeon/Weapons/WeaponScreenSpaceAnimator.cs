using System;
using Common.ScreenSpaceEffects;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class WeaponScreenSpaceAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int BottomLeftHash = Animator.StringToHash("bottom_left");
        private static readonly int BottomRightHash = Animator.StringToHash("bottom_right");
        
        public void PlayHit(ScreenSpaceDirection direction)
        {
            _animator.SetTrigger(direction switch
            {
                ScreenSpaceDirection.BottomLeft => BottomLeftHash,
                ScreenSpaceDirection.BottomRight => BottomRightHash,
                _=> throw new NotImplementedException()
            });
        }
    }
}