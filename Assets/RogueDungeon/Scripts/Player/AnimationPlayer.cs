using UnityEngine;

namespace RogueDungeon.Player
{
    public abstract class AnimationPlayer : MonoBehaviour, IAnimation
    {
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _loop;
        
        private float _playTime;
        private bool _isPlaying;

        public void Play()
        {
            _isPlaying = true;
            _playTime = 0;
            UpdatePlayback();
        }

        public void Stop() => 
            _isPlaying = false;

        private void Update()
        {
            if(!_isPlaying)
                return;
            
            _playTime += UnityEngine.Time.deltaTime;
            UpdatePlayback();
        }

        private void UpdatePlayback()
        {
            _animationClip.SampleAnimation(_target, _playTime);
            if (_playTime < _animationClip.length) 
                return;
            
            if (_loop)
                _playTime = 0;
            else
                Stop();
        }
    }
}