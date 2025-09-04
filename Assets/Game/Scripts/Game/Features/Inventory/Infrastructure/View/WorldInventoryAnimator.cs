using System;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class WorldInventoryAnimator : MonoBehaviour
    {
        private static readonly int PACK_HASH = Animator.StringToHash("pack");
        private static readonly int UNPACK_HASH = Animator.StringToHash("unpack");
        
        [SerializeField] private Animator _animator;
        private Action _callback;

        public void PlayUnpack()
        {
            // is gonna be called on both pack and unpack animations since they are the same animation played in reverse
            // so nullifying the callback in play unpack is crucial
            _callback = null;
            _animator.SetTrigger(UNPACK_HASH);
        }

        public void PlayPack(Action callback)
        {
            _callback = callback;
            _animator.SetTrigger(PACK_HASH);
        }

        // is gonna be called on both pack and unpack animations since they are the same animation played in reverse
        // so nullifying the callback in play unpack is crucial
        private void OnPackFinishedKeyframe()
        {
            _callback?.Invoke();
            _callback = null;
        }
    }
}