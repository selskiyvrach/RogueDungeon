using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
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