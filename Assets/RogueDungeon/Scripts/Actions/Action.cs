namespace RogueDungeon.Actions
{
    public abstract class Action
    {
        private readonly ActionConfig _config;
        private bool _hasStarted;
        private bool _isRewinding;

        public virtual bool IsFinished => _hasStarted && (!IsRewinding && CurrentFrame == _config.Frames || IsRewinding && CurrentFrame == 1);

        public int CurrentFrame { get; private set; }
        public int Frames => _config.Frames;
        public string AnimationName => _config.AnimationName;

        protected bool IsRewinding
        {
            get => _isRewinding;
            set
            {
                TryRaiseCallback();
                _isRewinding = value;
            }
        }

        protected Action(ActionConfig config) => 
            _config = config;

        public void Start()
        {
            _hasStarted = true;
            _isRewinding = false;
            CurrentFrame = 1;
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
        }

        private void TryRaiseCallback()
        {
            var keyframe = _config.GetKeyframe(CurrentFrame); 
            if(keyframe != null)
                OnKeyframe(keyframe);
        }
        protected abstract void OnKeyframe(string keyframe);
        protected virtual void OnStop()
        {
        }
    }
}