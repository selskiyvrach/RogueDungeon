using ITickable = Libs.Time.ITickable;

namespace Game.Libs.Time
{
    public class LifecycleTickableToTimeTickableAdapter : ITickable
    {
        private readonly global::Libs.Lifecycle.ITickable _regularTickable;
        private readonly TickOrder _tickOrder;

        public int TickOrder => (int)_tickOrder;

        public LifecycleTickableToTimeTickableAdapter(global::Libs.Lifecycle.ITickable regularTickable, TickOrder tickOrder)
        {
            _tickOrder = tickOrder;
            _regularTickable = regularTickable;
        }

        public void Tick(float deltaTime) => 
            _regularTickable.Tick(deltaTime);
        
        public bool TickableEquals(global::Libs.Lifecycle.ITickable tickable) => 
            _regularTickable.Equals(tickable);
    }
}