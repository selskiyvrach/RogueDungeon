using System;
using System.Collections.Generic;
using RogueDungeon.Characters.Commands;
using RogueDungeon.Items.Behaviours.WeaponWielder;
using RogueDungeon.Player.Behaviours.Dodge;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Player.Input
{
    public class PlayerInput : IDisposable, ICharacterCommands
    {
        private enum KeyState
        {
            Down,
            Held
        }
        
        private static readonly Dictionary<Type, (KeyCode keyCode, KeyState state, float coyoteTime)> Commands = new()
        {
            // [Input.MoveForward] = (KeyCode.W,KeyState.Held , 0),
            [typeof(IAttackCommand)] = (KeyCode.Mouse0, KeyState.Down, .5f),
            [typeof(IDodgeRightCommand)] = (KeyCode.D, KeyState.Down,.5f),
            [typeof(IDodgeLeftCommand)] = (KeyCode.A, KeyState.Down,.5f),
            // [Input.Block] = (KeyCode.Mouse1, KeyState.Held,.5f),
        };

        private readonly IDisposable _sub;
        private Type _currentInputCommand;
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
        }

        public bool TryConsume<T>() where T : ICharacterCommandDefinition
        {
            if (_currentInputCommand != typeof(T))
                return false;
            
            _currentInputCommand = null;
            _timeHeld = 0;
            _timeSinceReleased = Mathf.Infinity;
            return true;
        }

        private void ReadCommands()
        {
            var currentCommand = (Type)null;

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
                    _currentInputCommand = currentCommand;
                    _timeHeld = 0;
                }
                else
                    _timeHeld += Time.deltaTime;
            }
        }

        private void UpdateCoyoteTime()
        {
            if (_currentInputCommand == null) 
                return;
            
            _timeSinceReleased += Time.deltaTime;
            if (_timeSinceReleased >= Commands[_currentInputCommand].coyoteTime) 
                _currentInputCommand = null;
        }
    }
}