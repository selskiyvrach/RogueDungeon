using System;
using System.Collections.Generic;
using RogueDungeon.DebugTools;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Gameplay.InputCommands
{
    public class PlayerInput : ICommandsProvider, ICommandsConsumer, IDisposable
    {
        private enum KeyState
        {
            Down,
            Held
        }
        
        private static readonly Dictionary<Command, (KeyCode keyCode, KeyState state, float coyoteTime)> Commands = new()
        {
            [Command.MoveForward] = (KeyCode.W,KeyState.Held , 0),
            [Command.Attack] = (KeyCode.Mouse0, KeyState.Down,.15f),
            [Command.DodgeRight] = (KeyCode.D, KeyState.Down,.15f),
            [Command.DodgeLeft] = (KeyCode.A, KeyState.Down,.15f),
            [Command.Block] = (KeyCode.Mouse1, KeyState.Held,.15f),
        };

        private readonly IDisposable _sub;
        private Command? _lastDetectedCommand;
        private float _timeSinceReleased;
        private float _timeHeld;

        public PlayerInput() => 
            _sub = Observable.EveryUpdate().Subscribe(_ => Tick());

        public void Dispose() => 
            _sub?.Dispose();

        private void Tick()
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

            foreach (var (command, (code, state, coyoteTime)) in Commands)
            {
                if (! (state switch
                    {
                        KeyState.Down => Input.GetKeyDown(code),
                        KeyState.Held => Input.GetKey(code),
                        _ => throw new ArgumentOutOfRangeException()
                    } )) 
                    continue;
                
                _timeSinceReleased = 0;
                if (currentCommand != command)
                {
                    currentCommand = command;
                    _lastDetectedCommand = currentCommand;
                    _timeHeld = 0;
                }
                else
                    _timeHeld += Time.deltaTime;
            }
        }

        private void UpdateCoyoteTime()
        {
            if (_lastDetectedCommand == null) 
                return;
            
            _timeSinceReleased += Time.deltaTime;
            if (_timeSinceReleased >= Commands[(Command)_lastDetectedCommand].coyoteTime) 
                _lastDetectedCommand = null;
        }
    }
}