using Common.FSM;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class HasCommand : ICondition
    {
        private readonly IPlayerInput _playerInput;
        private readonly Command _command;

        public HasCommand(Command command, IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _command = command;
        }

        public bool IsMet() => 
            _playerInput.HasCommand(_command);
    }
}