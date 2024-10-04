using System.Collections.Generic;
using RogueDungeon.DebugTools;
using UnityEngine;

namespace RogueDungeon.Player.Commands
{
    public class CommandsReader : MonoBehaviour, ICommandsProvider, ICommandsConsumer
    {
        private static readonly Dictionary<Command, (KeyCode keyCode, float coyoteTime)> Commands = new()
        {
            [Command.MoveForward] = (KeyCode.W, 0),
            [Command.Attack] = (KeyCode.Mouse0, .5f),
            [Command.DodgeRight] = (KeyCode.D, .5f),
            [Command.DodgeLeft] = (KeyCode.A, .5f),
            [Command.Block] = (KeyCode.Mouse1, .15f),
        };

        private Command? _lastDetectedCommand;
        private float _timeSinceReleased;
        private float _timeHeld;

        private void Update()
        {
            UpdateCoyoteTime();
            ReadCommands();
            DebugHUD.SetData(this, "Current command: " + _lastDetectedCommand + (_timeHeld > 0.15f ? $"({_timeHeld:F2})" : ""));
        }

        public bool HasCommand(Command command) => 
            _lastDetectedCommand == command;

        public bool HasCommand(Command command, out float heldDuration)
        {
            heldDuration = _timeHeld;
            return HasCommand(command);
        }

        public void ConsumeCommandIfCurrent(Command command)
        {
            if (_lastDetectedCommand != command)
                return;
            
            _lastDetectedCommand = null;
            _timeHeld = 0;
            _timeSinceReleased = Mathf.Infinity;
        }

        private void ReadCommands()
        {
            var currentCommand = (Command?)null;

            foreach (var (command, (code, coyoteTime)) in Commands)
            {
                if (!Input.GetKey(code)) 
                    continue;
                
                _timeSinceReleased = 0;
                if (currentCommand != command)
                {
                    currentCommand = command;
                    _lastDetectedCommand = currentCommand;
                    _timeHeld = 0;
                }
                else
                    _timeHeld += UnityEngine.Time.deltaTime;
            }
        }

        private void UpdateCoyoteTime()
        {
            if (_lastDetectedCommand == null) 
                return;
            
            _timeSinceReleased += UnityEngine.Time.deltaTime;
            if (_timeSinceReleased >= Commands[(Command)_lastDetectedCommand].coyoteTime) 
                _lastDetectedCommand = null;
        }
    }
}