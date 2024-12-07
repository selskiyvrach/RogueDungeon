using Common.FSM;
using RogueDungeon.PlayerInput;

namespace RogueDungeon.Player
{
    public class ConsumeCommandStateEnterHandler : IStateEnterHandler
    {
        private readonly ICharacterInput _commandsConsumer;
        private readonly Command _command;

        public ConsumeCommandStateEnterHandler(ICharacterInput commandsConsumer, Command command)
        {
            _commandsConsumer = commandsConsumer;
            _command = command;
        }

        public void OnEnter() => 
            _commandsConsumer.ConsumeCommandIfCurrent(_command);
    }
}