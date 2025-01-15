namespace Common.Behaviours
{
    public abstract class Behaviour : IBehaviour
    {
        private bool _isEnabled;

        public bool IsEnabled => _isEnabled;

        public virtual void Enable() => 
            _isEnabled = true;

        public virtual void Disable() => 
            _isEnabled = false;
    }
}