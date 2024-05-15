using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RogueDungeon.Utils;
using UnityEngine;

namespace RogueDungeon.Characters
{
    public class Hivemind
    {
        private readonly List<AiCharacterController> _charactersInMoveOrder = new(3);
        private readonly CharactersManager _charactersManager;
        
        private AiCharacterController _currentCharacter;
        private int _currentCharacterIndex;
        private int _chillFrames;
        
        [CanBeNull] private AiCharacterController _characterBeingSwapped;
        private bool _isSwapping;
        private int _swapFrames;
        private readonly int _swapDuration = 15;

        public Hivemind(CharactersManager charactersManager)
        {
            _charactersManager = charactersManager;
            RefreshCharactersList();
        }

        public void Tick()
        {
            // do not proceed with all dead
            if(_charactersManager.AliveEnemies.Count == 0)
                // TODO(add chill frames reset, but it feels ok tbh, that enemies can start attack after some time)
                return;
            
            // do not do anything during swaps
            if (HandleSwapping()) 
                return;

            // if current char is performing its pattern
            if (_currentCharacter is { CurrentPattern: not null })
                return;

            // wait for chill time
            if (_currentCharacterIndex == _charactersInMoveOrder.Count)
            {
                if (_chillFrames-- == 0) 
                    RefreshCharactersList();
                return;
            }

            // pick next character
            _currentCharacter = null;
            while (_currentCharacter == null && _currentCharacterIndex < _charactersInMoveOrder.Count)
            {
                var nextChar = _charactersInMoveOrder[_currentCharacterIndex++];
                if(!nextChar.Character.Health.IsDead)
                    _currentCharacter = nextChar;
            }

            if (_currentCharacter == null)
                return;

            _currentCharacter.StartNewPattern();
            _chillFrames += (int)_currentCharacter.CurrentPattern.ChillFrames.GetValue();
            
            // if it's the last char pattern - finalize chill duration 
            if (_currentCharacterIndex == _charactersInMoveOrder.Count)
                _chillFrames /= _charactersInMoveOrder.Count;
        }

        private void StopCharactersActivity()
        {
            foreach (var controller in _charactersInMoveOrder) 
                controller.StopCurrentPattern();
        }

        private void RefreshCharactersList()
        {
            _charactersInMoveOrder.Clear();
            
            foreach (var character in _charactersManager.AllEnemies)
            {
                if(character.Health.IsDead)
                    continue;
                
                if (character.CombatState.Position != Position.Player)
                    _charactersInMoveOrder.Add((AiCharacterController)character.Controller);
            }

            _charactersInMoveOrder.Shuffle();
            _currentCharacterIndex = 0;
        }

        private bool HandleSwapping()
        {
            switch (_isSwapping)
            {
                // start swap
                case false when !_charactersManager.HasCharacterInPosition(Position.Frontline, out var frontliner):
                    StopCharactersActivity();
                    _characterBeingSwapped = _charactersManager.AliveEnemies.Select(n => n.Controller as AiCharacterController).ToArray().Shuffle()[0];
                    _swapFrames = _swapDuration;
                    _isSwapping = true;
                    return true;
                case false:
                    return false;
            }

            if (_swapFrames-- > 0)
            {
                var normalizedSwapTime = 1 - (float)_swapFrames / _swapDuration;
                var prevPos = _charactersManager.GetLocalPosForPosition(_characterBeingSwapped.Character.CombatState.Position);
                var targetPos = _charactersManager.GetLocalPosForPosition(Position.Frontline);
                _characterBeingSwapped.Character.GameObject.transform.localPosition = Vector3.Lerp(prevPos, targetPos, normalizedSwapTime);
                return true;
            }

            _characterBeingSwapped.Character.CombatState.Position = Position.Frontline;
            RefreshCharactersList();
            _isSwapping = false;
            return false;
        }
    }
}