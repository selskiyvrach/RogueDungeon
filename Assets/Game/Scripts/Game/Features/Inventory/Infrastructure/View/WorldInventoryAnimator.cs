using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class WorldInventoryAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject _animationRootObject;
        [SerializeField] private WorldInventoryConfig _config;

        public enum AnimatorState
        {
            None,
            Closed,
            Opening,
            Open,
            Closing,
        }
        
        private AnimatorState _stateBackingField;
        private float _animationPlayTime;

        public AnimatorState State
        {
            get => _stateBackingField;
            set
            {
                Assert.IsTrue(value != _stateBackingField);
                
                _stateBackingField = value;
                _animationPlayTime = 0;
            }
        }
        
        private void Awake()
        {
            State = AnimatorState.Closed;
            // _config.OpenAnimation.SampleAnimation(_animationRootObject, 0);
        }

        public void Unpack()
        {
            Assert.IsTrue(State == AnimatorState.Closed);
            gameObject.SetActive(true);
            State = AnimatorState.Open;
        }

        public void Pack()
        {
            Assert.IsTrue(State == AnimatorState.Open);
            State = AnimatorState.Closed;
        }

        public void Tick(float timeDelta)
        {
            // if (State is AnimatorState.Closed or AnimatorState.Open)
            //     return;
            //
            // var animationPosition = Mathf.Clamp01(_animationPlayTime / _config.OpenAnimationDuration);
            // if(State is AnimatorState.Closing)
            //     animationPosition = 1 - animationPosition;
            // _config.OpenAnimation.SampleAnimation(_animationRootObject, animationPosition);
            // _animationPlayTime += timeDelta;
            //
            // if(_animationPlayTime >= _config.OpenAnimationDuration)
            //     State = State == AnimatorState.Opening ? AnimatorState.Open : AnimatorState.Closed;
        }
    }
}