using System;
using System.Collections.Generic;
using Common.Animations;
using Common.DotNetUtils;
using UnityEditor;
using UnityEngine;

namespace RogueDungeon.Animations
{
    public class AnimationPlayer : MonoBehaviour, IAnimator
    {
        [SerializeField] private Animation _referenceToAnimation;
        
        private readonly Queue<(float time, string eventName)> _eventsToPlay = new();
        
        private AnimationClip[] _clips;
        private AnimationClip _clip;
        private float _duration;
        private bool _isLooped;
        private float _playTime;
        private float _timeScale;
        private IDisposable _updateSubscriber;

        public event Action<string> OnEvent;
        public bool IsFinished => _playTime >= _clip.length;

        private void Awake()
        {
            enabled = false;
            _clips = AnimationUtility.GetAnimationClips(_referenceToAnimation.ThrowIfNull().gameObject); 
        }

        public void Play(LoopedAnimationData loopedAnimationData)
        {
            _isLooped = true;
            var clip = _clips.Get(n => n.name == loopedAnimationData.Name);
            _duration = clip.length; 
            Play(clip);
        }

        public void Play(AnimationData animationData)
        {
            _duration = animationData.Duration;
            _isLooped = false;
            Play(_clips.Get(n => n.name == animationData.Name));
        }

        private void Play(AnimationClip clip)
        {
            _clip = clip;
            _timeScale = clip.length / _duration;
            _playTime = 0;
            PrepareEvents();
            UpdatePlayback();
            enabled = true;
        }

        private void Update()
        {
            _playTime += Time.deltaTime * _timeScale;
            EvaluateEvents();
            UpdatePlayback();
        }

        private void UpdatePlayback()
        {
            _clip.SampleAnimation(gameObject, _playTime);
            
            if (!IsFinished)
                return;

            if (_isLooped)
            {
                _playTime = 0;
                PrepareEvents();
            }
            else
                enabled = false;
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