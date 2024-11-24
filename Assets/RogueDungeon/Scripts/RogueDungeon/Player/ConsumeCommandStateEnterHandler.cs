using Common.FSM;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class ConsumeCommandStateEnterHandler : IStateEnterHandler
    {
        private readonly ICommandsConsumer _commandsConsumer;
        private readonly Command _command;

        public ConsumeCommandStateEnterHandler(ICommandsConsumer commandsConsumer, Command command)
        {
            _commandsConsumer = commandsConsumer;
            _command = command;
        }

        public void OnEnter() => 
            _commandsConsumer.ConsumeCommandIfCurrent(_command);
    }
}