using JetBrains.Annotations;
using RogueDungeon.Characters;
using RogueDungeon.Data;
using UnityEngine.Assertions;

namespace RogueDungeon.Actions
{
    public abstract class Action
    {
        private readonly IActionConfig _config;
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

        public virtual bool IsFinished => _hasStarted && (!IsRewinding && CurrentFrame == _config.Frames || IsRewinding && CurrentFrame == 1);

        public int CurrentFrame { get; private set; }

        public int Frames => _config.Frames;

        public string AnimationName => _config.AnimationName;

        /// <summary>
        /// The character for whom the action is being executed at the moment. Null if the action is not being executed at the moment
        /// </summary>
        [CanBeNull]
        protected Character Character { get; private set; }

        protected Action(IActionConfig config)
        {
            Assert.IsNotNull(config);
            _config = config;
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
                case false when CurrentFrame == _config.Frames:
                case true when CurrentFrame == 1:
                    return;
            }

            CurrentFrame += IsRewinding ? -1 : 1;
            TryRaiseCallback();
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
            var keyframe = _config.GetKeyframe(CurrentFrame); 
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