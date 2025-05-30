using System;
using Libs.Lifecycle;
using Libs.Time;
using ITickable = Libs.Time.ITickable;

namespace Game.Libs.Time
{
    public class ObjectTicker : IDisposable, IInitializable
    {
        private readonly ITimeService _time;
        private readonly SimpleTickableToOrderedTickableAdapter _adapter;

        public ObjectTicker(ITimeService time, global::Libs.Lifecycle.ITickable tickable, TickOrder tickOrder = TickOrder.Base)
        {
            _time = time;
            _adapter = new SimpleTickableToOrderedTickableAdapter(tickable, tickOrder);
        }

        public void Initialize() => 
            _time.Register(_adapter);

        public void Dispose() => 
            _time.Unregister(_adapter);
    }

    public class SimpleTickableToOrderedTickableAdapter : ITickable
    {
        private readonly global::Libs.Lifecycle.ITickable _regularTickable;
        private readonly TickOrder _tickOrder;

        public int TickOrder => (int)_tickOrder;

        public SimpleTickableToOrderedTickableAdapter(global::Libs.Lifecycle.ITickable regularTickable, TickOrder tickOrder)
        {
            _tickOrder = tickOrder;
            _regularTickable = regularTickable;
        }

        public void Tick(float deltaTime) => 
            _regularTickable.Tick(deltaTime);
    }
}