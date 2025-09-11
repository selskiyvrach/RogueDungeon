using System;
using Game.Features.Inventory.App.Presenters;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class ItemViewAnimator : MonoBehaviour, IItemViewAnimator
    {
        private static readonly int DROP_HASH = Animator.StringToHash("drop");
        [SerializeField] private Animator _animator;
        
        public event Action OnAnimationFinished;

        public void PlayDropped()
        {
            _animator.enabled = true;
            _animator.SetTrigger(DROP_HASH);
        }

        private void OnDroppedFinishedPlayingKeyframe()
        {
            _animator.enabled = false;
            OnAnimationFinished?.Invoke();
        }
    }
}