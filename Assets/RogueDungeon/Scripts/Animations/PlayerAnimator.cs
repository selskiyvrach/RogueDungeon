using System;
using System.Collections;
using UnityEngine;

namespace RogueDungeon.Animations
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Animator _animator;
        private Coroutine _changeSpeedRoutine;
        
        private static readonly int Speed = UnityEngine.Animator.StringToHash("Speed");

        public event Action<string> OnKeyFrame;

        public void PlayState(string stateName, float duration)
        {
            if (!_animator.HasState(0, UnityEngine.Animator.StringToHash(stateName)))
            {
                Debug.LogError($"No state with name {stateName} on {name}");
                return;
            }

            _animator.SetTrigger(stateName);
            if(_changeSpeedRoutine != null)
                StopCoroutine(_changeSpeedRoutine);
            _changeSpeedRoutine = StartCoroutine(ChangeCurrentStateDuration(duration));
        }

        private IEnumerator ChangeCurrentStateDuration(float targetDuration)
        {
            yield return null;
            _animator.SetFloat(Speed, targetDuration / _animator.GetCurrentAnimatorStateInfo(0).length);
            _changeSpeedRoutine = null;
        }

        // called from the animator
        private void ReceiveKeyFrame(string keyframe) => 
            OnKeyFrame?.Invoke(keyframe);
    }
}