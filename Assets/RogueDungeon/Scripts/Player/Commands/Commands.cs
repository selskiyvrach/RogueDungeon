using UnityEngine;

namespace RogueDungeon.Player.Commands
{
    public class Commands : ICommandsProvider
    {
        private Command _command;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                _command = Command.Attack;
            if (Input.GetKeyDown(KeyCode.Mouse1))
                _command = Command.Block;
            if (Input.GetKeyDown(KeyCode.A))
                _command = Command.DodgeLeft;
            if (Input.GetKeyDown(KeyCode.D))
                _command = Command.DodgeRight;
        }

        public bool HasCommand(Command command) => 
            _command == command;
    }
}