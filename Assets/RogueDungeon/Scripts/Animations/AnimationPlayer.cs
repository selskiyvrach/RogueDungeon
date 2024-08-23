using System;
using System.Collections.Generic;
using System.Linq;
using RogueDungeon.DebugTools;
using RogueDungeon.StateMachine;
using UniRx;
using UnityEngine;
using Logger = RogueDungeon.DebugTools.Logger;

namespace RogueDungeon.Animations
{
    public class DummyStringAnimationEventsListener : MonoBehaviour
    {
        public void DummyMethodToDeclareStringParameter(string parameter)
        {
        }
    }

    [Serializable]
    public class AnimationPlayerToFinishableAdapter : IFinishable
    {
        private AnimationPlayer _animationPlayer;
        public bool IsFinished => _animationPlayer.IsFinished;

        public AnimationPlayerToFinishableAdapter(AnimationPlayer animationPlayer)
        {
            _animationPlayer = animationPlayer;
            if (_animationPlayer.IsLooped) 
                Logger.LogError(this, "Animation should not be looped in order to be used in this context");
        }
    }

    public class AnimationEventsDispatcher
    {
        private readonly List<(float time, List<Action> handlers)> _allKnownHandlers;
        private readonly Queue<(float time, List<Action> handlers)> _handlersToPlay = new();

        public AnimationEventsDispatcher(IEnumerable<AnimationEvent> events, IEnumerable<(string name, Action handler)> handlers)
        {
            var eventsList = events?.ToList();
            var handlersList = handlers?.ToList();

            if (eventsList.IsNullOrEmpty() || handlersList.IsNullOrEmpty())
                return;

            _allKnownHandlers = eventsList.Select(e => (e.time, handlersList.Where(h => h.name == e.stringParameter).Select(n => n.handler).ToList())).ToList();

            var mismatchedItems = eventsList.Select(n => n.stringParameter).NonSharedItems(handlersList.Select(n => n.name)).ToListOrNull();
            if (mismatchedItems != null)
                Logger.LogError(this, "Mismatched animation events and handlers by names: " + string.Join(", ", mismatchedItems));
        }

        public void Start()
        {
            if (_allKnownHandlers.IsNullOrEmpty())
                return;

            _handlersToPlay.Clear();

            foreach (var handler in _allKnownHandlers)
                _handlersToPlay.Enqueue(handler);
        }

        public void Evaluate(float time)
        {
            if (_handlersToPlay.IsNullOrEmpty())
                return;

            while (_handlersToPlay.TryPeek(out var peekedHandlers) && peekedHandlers.time < time)
            {
                foreach (var handler in _handlersToPlay.Dequeue().handlers)
                    handler?.Invoke();
            }
        }
    }

    [Serializable]
    public class AnimationData
    {
        
    }
    
    // animation factory 
        // data
        // root object
        // handlers by type

    [Serializable]
    public class AnimationPlayer : IAnimation, IDebugName
    {
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private GameObject _target;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private bool _loop;
       
        private readonly AnimationEventsDispatcher _eventsDispatcher = new(null, null);
        private float _playTime;
        private float _timeScale;
        private IDisposable _updateSubscriber;

        public string DebugName => $"[Animation player] animation name: {_animationClip?.name}";
        public bool IsFinished => _playTime >= _animationClip.length;
        public bool IsLooped => _loop;
        
        public void Play()
        {
            
            
            
            
            // get handlers
            // get events
            
            
            
            
            
            
            
            _playTime = 0;
            _timeScale = _animationClip.length / _duration;
            _updateSubscriber = Observable.EveryUpdate().Subscribe(_ => OnUpdate());
            _eventsDispatcher.Start();
            UpdatePlayback();
        }

        public void Stop() =>
            _updateSubscriber?.Dispose();

        private void OnUpdate()
        {
            _playTime += UnityEngine.Time.deltaTime * _timeScale;
            UpdatePlayback();
            _eventsDispatcher.Evaluate(_playTime);
        }

        private void Restart() => 
            _eventsDispatcher.Start();

        private void UpdatePlayback()
        {
            _animationClip.SampleAnimation(_target, _playTime);
            
            if (!IsFinished)
                return;

            if (_loop)
            {
                _playTime = 0;
                Restart();
            }
            else
                Stop();
        }
    }
}