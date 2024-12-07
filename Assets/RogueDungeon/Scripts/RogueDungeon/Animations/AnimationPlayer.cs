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
        private AnimationConfig _config;
        private AnimationRootObject _target;
        private readonly Queue<(float time, string eventName)> _eventsToPlay = new();
        private float _duration;

        private float _playTime;
        private float _timeScale;
        private IDisposable _updateSubscriber;
        public ISubject<string> OnEvent { get; } = new Subject<string>();

        public string DebugName => $"[Animation player] animation name: {_config?.AnimationClip?.name}";

        public bool IsFinished => _playTime >= _config.AnimationClip.length;

        public bool IsLooped => _config.Loop;

        public void Play(AnimationConfig config, AnimationRootObject target) =>
            Play(config, target, config.AnimationClip.length);
        
        public void Play(AnimationConfig config, AnimationRootObject target, float duration)
        {
            _target = target;
            _config = config;
            _playTime = 0;
            _timeScale = _config.AnimationClip.length / duration;
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
            _config.AnimationClip.SampleAnimation(_target.gameObject, _playTime);
            
            if (!IsFinished)
                return;

            if (_config.Loop)
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
            for (var i = 0; i < _config.AnimationClip.events.Length; i++)
            {
                var @event = _config.AnimationClip.events[i];
                if (@event.stringParameter.IsNullOrEmpty())
                    Debug.LogError($"Invalid event name at index {i} in animation clip {_config.AnimationClip.name}");
                else
                    _eventsToPlay.Enqueue((@event.time, @event.stringParameter));
            }
        }
    }
}