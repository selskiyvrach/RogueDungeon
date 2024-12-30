using Zenject;

namespace Common.Behaviours
{
    public class BehaviourAutorunner<T> : IInitializable where T : IBehaviour
    {
        private readonly T _behaviour;

        public BehaviourAutorunner(T behaviour) => 
            _behaviour = behaviour;

        [Inject]
        public void Initialize() => 
            _behaviour.Enable();
    }
}