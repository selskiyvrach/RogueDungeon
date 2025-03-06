using System;
using System.Collections.Generic;
using Common.UtilsDotNet;
using UnityEngine;

namespace Common.Animations
{
    public sealed class AnimationPlayer : MonoBehaviour, IAnimator
    {
        [SerializeField] private Animation _referenceToAnimation;
        [SerializeField] private GameObject _animatedObject;
        
        private readonly Queue<(float time, string eventName)> _eventsToPlay = new();
        
        private AnimationClip _clip;
        private float _duration;
        private bool _isLooped;
        private float _playTime;
        private float _timeScale;
        private IDisposable _updateSubscriber;

        public event Action<string> OnEvent;
        public bool IsFinished => _playTime >= _clip.length;

        public void Play(AnimationData animationData)
        {
            _duration = animationData.Duration;
            _isLooped = false;
            _clip = animationData.Clip;
            _timeScale = _clip.length / _duration;
            _playTime = 0;
            PrepareEvents();
            UpdatePlayback();
        }

        private void Update()
        {
            if(_clip == null)
                return;
            
            _playTime += Time.deltaTime * _timeScale;
            EvaluateEvents();
            UpdatePlayback();
        }

        private void UpdatePlayback()
        {
            _clip.SampleAnimation(_animatedObject, _playTime);
            
            if (!IsFinished)
                return;

            if (_isLooped)
            {
                _playTime = 0;
                PrepareEvents();
            }
            else
                _clip = null;
        }

        private void EvaluateEvents()
        {
            while (_eventsToPlay.TryPeek(out var peekedEvent) && peekedEvent.time < _playTime) 
                OnEvent?.Invoke(_eventsToPlay.Dequeue().eventName);
        }

        private void PrepareEvents()
        {
            _eventsToPlay.Clear();
            for (var i = 0; i < _clip.events.Length; i++)
            {
                var @event = _clip.events[i];
                if (@event.stringParameter.IsNullOrEmpty())
                    Debug.LogError($"Invalid event name at index {i} in animation clip {_clip.name}");
                else
                    _eventsToPlay.Enqueue((@event.time, @event.stringParameter));
            }
        }
    }
}