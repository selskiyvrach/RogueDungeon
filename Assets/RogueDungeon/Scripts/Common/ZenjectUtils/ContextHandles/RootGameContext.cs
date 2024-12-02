using Zenject;

namespace Common.ZenjectUtils.ContextHandles
{
    public class GameContextHandle : ContextHandle
    {
        
    }
    
    public abstract class ContextHandle : IContextHandle
    {
        [Inject] public DiContainer Container { get; }
    }
}