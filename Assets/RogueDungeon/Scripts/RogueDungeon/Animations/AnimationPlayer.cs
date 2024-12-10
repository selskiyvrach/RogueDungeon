using System;
using System.Collections.Generic;
using Common.DebugTools;
using Common.DotNetUtils;
using Common.GameObjectMarkers;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Animations
{
    public class AnimationPlayer : IDebugName
    {
        private readonly Queue<(float time, string eventName)> _eventsToPlay = new();
        
        private AnimationRootObject _target;
        private AnimationClip _clip;
        private float _duration;
        private bool _isLooped;
        private float _playTime;
        private float _timeScale;
        private IDisposable _updateSubscriber;
        public ISubject<string> OnEvent { get; } = new Subject<string>();

        public string DebugName => $"[Animation player] animation name: { _clip?.name}";

        public bool IsFinished => _playTime >= _clip.length;

        public void Play(AnimationClip clip, AnimationRootObject target, float? duration = null, bool isLooped = false)
        {
            _clip = clip;
            _target = target;
            _duration = duration ?? _clip.length;
            _isLooped = isLooped;
            _playTime = 0;
            _timeScale = _clip.length / _duration;
            _updateSubscriber?.Dispose();
            _updateSubscriber = Observable.EveryUpdate().Subscribe(_ => OnUpdate());
            PrepareEvents();
            UpdatePlayback();
        }

        public void Stop() =>
            _updateSubscriber?.Dispose();

        private void OnUpdate()
        {
            _playTime += Time.deltaTime * _timeScale;
            EvaluateEvents();
            UpdatePlayback();
        }

        private void UpdatePlayback()
        {
            _clip.SampleAnimation(_target.gameObject, _playTime);
            
            if (!IsFinished)
                return;

            if (_isLooped)
            {
                _playTime = 0;
                PrepareEvents();
            }
            else
                Stop();
        }

        private void EvaluateEvents()
        {
            while (_eventsToPlay.TryPeek(out var peekedEvent) && peekedEvent.time < _playTime) 
                OnEvent.OnNext(_eventsToPlay.Dequeue().eventName);
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