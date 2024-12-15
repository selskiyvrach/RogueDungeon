using Zenject;

namespace Common.UtilsZenject.ContextHandles
{
    public class GameContextHandle : ContextHandle
    {
        
    }
    
    public abstract class ContextHandle : IContextHandle
    {
        [Inject] public DiContainer Container { get; }
    }
}