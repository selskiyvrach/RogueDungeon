using System.Collections.Generic;
using RogueDungeon.DebugTools;
using UnityEngine;

namespace RogueDungeon.Player.Commands
{
    public class CommandsReader : MonoBehaviour, ICommandsProvider, ICommandsConsumer
    {
        private readonly Dictionary<Command, KeyCode> _commandCodes = new ()
        {
            [Command.Attack] = KeyCode.Mouse0,
            [Command.DodgeLeft] = KeyCode.A,
            [Command.DodgeRight] = KeyCode.D,
        };

        private Command? _currentCommand;

        private void Update()
        {
            ReadCommands();
            DebugHUD.SetData(this, "Current command: " + _currentCommand);
        } 

        private void ReadCommands()
        {
            // walk input
            if (_currentCommand == Command.MoveForward)
                _currentCommand = null;
            if (Input.GetKey(KeyCode.W))
                _currentCommand = Command.MoveForward;
            
            // other inputs
            foreach (var (command, keyCode) in _commandCodes)
            {
                if (Input.GetKeyDown(keyCode))
                    _currentCommand = command;
            }

            // block input
            if(Input.GetKey(KeyCode.Mouse1))
                _currentCommand = Command.HoldBlock;
            else if (_currentCommand == Command.HoldBlock)
                _currentCommand = null;
        }

        public bool HasCommand(Command command) => 
            _currentCommand == command;

        public void ConsumeCommandIfCurrent(Command command)
        {
            if (_currentCommand == command)
                _currentCommand = null;
        }
    }
}