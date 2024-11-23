using System;
using System.Collections.Generic;
using RogueDungeon.Services.DebugTools;
using RogueDungeon.Services.Extensions;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Animations
{
    public class AnimationPlayer : IAnimation, IDebugName
    {
        private readonly AnimationConfig _config;
        private readonly AnimationTarget _target;
        private readonly Queue<(float time, string eventName)> _eventsToPlay = new();
        private readonly AnimationClip _animationClip;
        private readonly bool _loop;
        private readonly float _duration;

        private float _playTime;
        private float _timeScale;
        private IDisposable _updateSubscriber;
        public ISubject<string> OnEvent { get; } = new Subject<string>();

        public string DebugName => $"[Animation player] animation name: {_animationClip?.name}";

        public bool IsFinished => _playTime >= _animationClip.length;

        public bool IsLooped => _loop;

        public AnimationPlayer(AnimationConfig config, AnimationTarget target)
        {
            _target = target;
            _config = config;
            _animationClip = _config.AnimationClip;
            _loop = _config.Loop;
            _duration = _config.Duration;
        }

        public void Play()
        {
            _playTime = 0;
            _timeScale = _animationClip.length / _duration;
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
            _animationClip.SampleAnimation(_target.GameObject, _playTime);
            
            if (!IsFinished)
                return;

            if (_loop)
            {
                _playTime = 0;
                PrepareEvents();
            }
            else
                Stop();
        }

        public void EvaluateEvents()
        {
            while (_eventsToPlay.TryPeek(out var peekedEvent) && peekedEvent.time < _playTime) 
                OnEvent.OnNext(_eventsToPlay.Dequeue().eventName);
        }

        private void PrepareEvents()
        {
            _eventsToPlay.Clear();
            for (var i = 0; i < _animationClip.events.Length; i++)
            {
                var @event = _animationClip.events[i];
                if (@event.stringParameter.IsNullOrEmpty())
                    Debug.LogError($"Invalid event name at index {i} in animation clip {_animationClip.name}");
                else
                    _eventsToPlay.Enqueue((@event.time, @event.stringParameter));
            }
        }
    }
}