using Zenject;
using Lifecycle_ITickable = Libs.Lifecycle.ITickable;

namespace Game.Libs.Time
{
    public class ObjectTickerFactory : PlaceholderFactory<Lifecycle_ITickable, TickOrder, ObjectTicker>
    {
        
    }
}