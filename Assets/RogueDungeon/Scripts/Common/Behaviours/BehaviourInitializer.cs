using Zenject;

namespace Common.Behaviours
{
    public class BehaviourInitializer<T> : IInitializable where T : IBehaviour
    {
        private readonly T _behaviour;

        public BehaviourInitializer(T behaviour) => 
            _behaviour = behaviour;

        [Inject]
        public void Initialize() => 
            _behaviour.Enable();
    }
}