using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventory : MonoBehaviour
    {
        [SerializeField] private GameObject _animationRootObject;
        [SerializeField] private WorldInventoryConfig _config;
        
        private enum State
        {
            None,
            Closed,
            Opening,
            Open,
            Closing,
        }

        private State _stateBackingField;
        private float _animationPlayTime;

        private State _state
        {
            get => _stateBackingField;
            set
            {
                Assert.IsTrue(value != _stateBackingField);
                
                _stateBackingField = value;
                _animationPlayTime = 0;
            }
        }
        
        public bool IsOpen => _state == State.Open;

        private void Awake()
        {
            _state = State.Closed;
            _config.OpenAnimation.SampleAnimation(_animationRootObject, 0);
        }

        public void Unpack()
        {
            Assert.IsTrue(_state == State.Closed);
            gameObject.SetActive(true);
            _state = State.Opening;
        }

        public void Pack()
        {
            Assert.IsTrue(_state == State.Open);
            _state = State.Closing;
        }

        public void Tick(float timeDelta)
        {
            if (_state is State.Closed or State.Open)
                return;
            
            var animationPosition = Mathf.Clamp01(_animationPlayTime / _config.OpenAnimationDuration);
            if(_state is State.Closing)
                animationPosition = 1 - animationPosition;
            _config.OpenAnimation.SampleAnimation(_animationRootObject, animationPosition);
            _animationPlayTime += timeDelta;
            
            if(_animationPlayTime >= _config.OpenAnimationDuration)
                _state = _state == State.Opening ? State.Open : State.Closed;
        }
    }
}