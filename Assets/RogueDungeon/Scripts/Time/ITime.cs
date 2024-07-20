using System;

namespace RogueDungeon.Time
{
    public interface ITime
    {
        void EachTick(Action callback, int tickOrder = TickOrders.DEFAULT);
        void StopEachTick(Action callback);
        float TimeDelta { get; }
    }
}