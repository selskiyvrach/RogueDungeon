using JetBrains.Annotations;
using RogueDungeon.Characters;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Actions
{
    public abstract class Action
    {
        public IActionConfig Config { get; }
        private bool _hasStarted;
        private bool _isRewinding;

        protected bool IsRewinding
        {
            get => _isRewinding;
            set
            {
                _isRewinding = value;
                TryRaiseCallback();
            }
        }

        public virtual bool IsFinished => _hasStarted && (!IsRewinding && CurrentFrame == Config.Frames || IsRewinding && CurrentFrame == 1);

        public int CurrentFrame { get; private set; }

        public int Frames => Config.Frames;

        public string AnimationName => Config.AnimationName;

        public bool Cycle => Config.Cycle;

        /// <summary>
        /// The character for whom the action is being executed at the moment. Null if the action is not being executed at the moment
        /// </summary>
        [CanBeNull]
        protected Character Character { get; private set; }

        protected Action(IActionConfig config)
        {
            Assert.IsNotNull(config);
            Config = config;
        }

        public void Start(Character character)
        {
            Character = character;
            _hasStarted = true;
            _isRewinding = false;
            CurrentFrame = 1;
            OnStarted();
            TryRaiseCallback();
        }

        public void Tick()
        {
            switch (IsRewinding)
            {
                case false when CurrentFrame == Config.Frames:
                case true when CurrentFrame == 1:
                    return;
            }

            CurrentFrame += IsRewinding ? -1 : 1;
            TryRaiseCallback();
            if (IsFinished && Cycle)
                CurrentFrame = IsRewinding ? Config.Frames : 0;
        }

        public void Stop()
        {
            _hasStarted = false;
            OnStop();
            Character = null;
        }

        public virtual void OnCommand(string command)
        {
        }

        private void TryRaiseCallback()
        {
            var keyframe = Config.GetKeyframe(CurrentFrame); 
            if(keyframe != null)
                OnKeyframe(keyframe);
        }

        protected abstract void OnKeyframe(string keyframe);

        protected virtual void OnStarted()
        {
        }

        protected virtual void OnStop()
        {
        }
    }
}