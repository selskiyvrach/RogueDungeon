using Common.FSM;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class ConsumeCommandStateEnterHandler : IStateEnterHandler
    {
        private readonly IPlayerInput _commandsConsumer;
        private readonly Command _command;

        public ConsumeCommandStateEnterHandler(IPlayerInput commandsConsumer, Command command)
        {
            _commandsConsumer = commandsConsumer;
            _command = command;
        }

        public void OnEnter() => 
            _commandsConsumer.ConsumeCommandIfCurrent(_command);
    }
}