using RogueDungeon.Entities.Properties;
using RogueDungeon.Player;
using RogueDungeon.Services.Events;

namespace RogueDungeon.Entities
{
    public class DodgeEventHandler : IEventHandler<DodgeEvent>
    {
        private readonly Property<DodgeState> _dodgeState;

        public DodgeEventHandler(Property<DodgeState> dodgeState) => 
            _dodgeState = dodgeState;

        public void Handle(DodgeEvent @event) => 
            _dodgeState.Value = @event.ToDodgeState();
    }
}