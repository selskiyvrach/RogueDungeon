using Zenject;

namespace Common.UtilsZenject.ContextHandles
{
    public interface IContextHandle
    {
        DiContainer Container { get; }
    }
}