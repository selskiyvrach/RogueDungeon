using Libs.Utils.DotNet;
using ITickable = Libs.Time.ITickable;

namespace Game.Libs.Time
{
    public class LifecycleTickableToTimeTickableAdapter : ITickable
    {
        private readonly TickOrder _tickOrder;

        public global::Libs.Lifecycle.ITickable Tickable { get; }
        public int TickOrder => (int)_tickOrder;

        public LifecycleTickableToTimeTickableAdapter(global::Libs.Lifecycle.ITickable regularTickable, TickOrder tickOrder)
        {
            _tickOrder = tickOrder;
            Tickable = regularTickable.ThrowIfNull();
        }

        public void Tick(float deltaTime) => 
            Tickable.Tick(deltaTime);

        protected bool Equals(LifecycleTickableToTimeTickableAdapter other) => 
            Equals(Tickable, other.Tickable);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LifecycleTickableToTimeTickableAdapter)obj);
        }

        public override int GetHashCode() => 
            Tickable != null ? Tickable.GetHashCode() : 0;
    }
}