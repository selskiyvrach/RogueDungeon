using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace RogueDungeon.PlayerInput
{
    public class CharacterInput : IDisposable, IInput
    {
        private enum KeyState
        {
            Down,
            Held
        }
        
        private static readonly Dictionary<Input, (KeyCode keyCode, KeyState state, float coyoteTime)> Commands = new()
        {
            [Input.MoveForward] = (KeyCode.W,KeyState.Held , 0),
            [Input.Attack] = (KeyCode.Mouse0, KeyState.Down, .5f),
            [Input.DodgeRight] = (KeyCode.D, KeyState.Down,.5f),
            [Input.DodgeLeft] = (KeyCode.A, KeyState.Down,.5f),
            [Input.Block] = (KeyCode.Mouse1, KeyState.Held,.5f),
        };

        private readonly IDisposable _sub;
        private Input? _input;
        private float _timeSinceReleased;
        private float _timeHeld;

        public CharacterInput() => 
            _sub = Observable.EveryUpdate().Subscribe(_ => Tick());

        public void Dispose() => 
            _sub?.Dispose();

        private void Tick()
        {
            UpdateCoyoteTime();
            ReadCommands();
        }

        public bool TryConsume(Input input)
        {
            if (_input != input)
                return false;
            
            _input = null;
            _timeHeld = 0;
            _timeSinceReleased = Mathf.Infinity;
            return true;
        }

        private void ReadCommands()
        {
            var currentCommand = (Input?)null;

            foreach (var (command, (code, state, coyoteTime)) in Commands)
            {
                if (! (state switch
                    {
                        KeyState.Down => UnityEngine.Input.GetKeyDown(code),
                        KeyState.Held => UnityEngine.Input.GetKey(code),
                        _ => throw new ArgumentOutOfRangeException()
                    } )) 
                    continue;
                
                _timeSinceReleased = 0;
                if (currentCommand != command)
                {
                    currentCommand = command;
                    _input = currentCommand;
                    _timeHeld = 0;
                }
                else
                    _timeHeld += Time.deltaTime;
            }
        }

        private void UpdateCoyoteTime()
        {
            if (_input == null) 
                return;
            
            _timeSinceReleased += Time.deltaTime;
            if (_timeSinceReleased >= Commands[(Input)_input].coyoteTime) 
                _input = null;
        }
    }
}