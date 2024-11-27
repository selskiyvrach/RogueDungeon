using Common.Events;
using RogueDungeon.Player;

namespace RogueDungeon.Entities
{
    public class DodgeEventHandler : IEventHandler<DodgeEvent>
    {
        private readonly IDodger _dodger;

        public DodgeEventHandler(IDodger dodger) => 
            _dodger = dodger;

        public void HandleEvent(DodgeEvent @event)
        {
            if(@event.State == DodgeEvent.DodgeState.Started)
                _dodger.StartDodge(@event.Direction);
            else
                _dodger.FinishDodge();
        }
    }
}