using Zenject;

namespace Common.ZenjectUtils.ContextHandles
{
    public interface IContextHandle
    {
        DiContainer Container { get; }
    }
}