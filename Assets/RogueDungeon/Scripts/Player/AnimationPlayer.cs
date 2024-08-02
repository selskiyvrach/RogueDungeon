using System;
using System.Collections.Generic;
using System.Linq;
using RogueDungeon.StateMachine;
using UnityEngine;

namespace RogueDungeon.Player
{
    public abstract class AnimationPlayer : MonoBehaviour, IAnimation, IFinishable
    {
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _loop;
        
        private struct EventData
        {
            public int Index;
            public float Time;
        }
        
        private readonly Stack<EventData> _animationEvents = new();
        
        private float _playTime;
        private bool _isPlaying;

        public bool IsFinished => _playTime >= _animationClip.length;

        public void Play()
        {
            _isPlaying = true;
            _playTime = 0;
            PrepareEvents();
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
            CheckAndFireEvents();
        }

        private void CheckAndFireEvents()
        {
            if(!_animationEvents.TryPeek(out var @event))
                return;
            if (@event.Time < _playTime - UnityEngine.Time.deltaTime * .5f)
                OnEvent(_animationEvents.Pop().Index);
        }

        protected virtual void OnEvent(int eventIndex)
        {
        }

        private void PrepareEvents()
        {
            _animationEvents.Clear();
            for (var i = _animationClip.events.Length - 1; i >= 0; i--) 
                _animationEvents.Push(new EventData { Time = _animationClip.events[i].time, Index = i});
        }

        private void UpdatePlayback()
        {
            _animationClip.SampleAnimation(_target, _playTime);
            
            if (!IsFinished)
                return;

            if (_loop)
            {
                _playTime = 0;
                PrepareEvents();
            }
            else
            {
                while (_animationEvents.Any()) 
                    OnEvent(_animationEvents.Pop().Index); 
                Stop();
            }

        }
    }
}