using System;
using Common.Events;
using RogueDungeon.Collisions;

namespace RogueDungeon.Player
{
    public class DodgeHandler : IDodger, IEventHandler<DodgeEvent>
    {
        public DodgeState DodgeState { get; private set; }

        public void HandleEvent(DodgeEvent @event) =>
            DodgeState = @event.State;

        public Positions ToPlayerPosition() =>
            DodgeState switch
            {
                DodgeState.None => Positions.PlayerDefault,
                DodgeState.Left => Positions.PlayerDodgeLeft,
                DodgeState.Right => Positions.PlayerDodgeRight,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}