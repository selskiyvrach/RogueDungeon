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
        private readonly int _swapDuration = 30;

        public Hivemind(CharactersManager charactersManager)
        {
            _charactersManager = charactersManager;
            RefreshCharactersList();
        }

        public void Tick()
        {
            if(_charactersManager.AllCharacters.All(n => n.Health.IsDead))
                return;
            
            foreach (var character in _charactersManager.AllCharacters.Where(n => n.CombatState.Position != Positions.Player)) 
                character.Controller.Tick();

            if (HandleSwapping()) 
                return;

            if (_currentCharacter is { CurrentPattern: not null })
                return;

            if (_currentCharacterIndex == _charactersInMoveOrder.Count)
            {
                if (_chillFrames-- == 0) 
                    RefreshCharactersList();

                return;
            }

            _currentCharacter = null;
            while (_currentCharacter == null && _currentCharacterIndex < _charactersInMoveOrder.Count)
            {
                var nextChar = _charactersInMoveOrder[_currentCharacterIndex++];
                if(!nextChar.Character.Health.IsDead)
                    _currentCharacter = nextChar;
            }
            if(_currentCharacter == null)
                return;
            _currentCharacter.StartNewPattern();
            _chillFrames += (int)_currentCharacter.CurrentPattern.ChillFrames.GetValue();
            
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
            
            foreach (var character in _charactersManager.AllCharacters)
            {
                if(character.Health.IsDead)
                    continue;
                
                if (character.CombatState.Position != Positions.Player)
                    _charactersInMoveOrder.Add((AiCharacterController)character.Controller);
            }

            _charactersInMoveOrder.Shuffle();
            _currentCharacterIndex = 0;
        }

        private bool HandleSwapping()
        {
            if (!_isSwapping && FrontlineNeedsReplacement())
            {
                StopCharactersActivity();
                if(_charactersManager.HasEnemyInPosition(Positions.Frontline, out var oldFrontliner))
                    oldFrontliner.CombatState.Position = 0;

                if (_charactersManager.AllCharacters.Where(n => n.CombatState.Position != Positions.Player)
                    .All(n => n.Health.IsDead))
                    return false;
                
                _characterBeingSwapped = _charactersInMoveOrder.Where(n => !n.Character.Health.IsDead).ToArray().Shuffle()[0];
                _swapFrames = _swapDuration;
                _isSwapping = true;
                return true;
            }

            if (!_isSwapping) 
                return false;
            
            if (_swapFrames-- > 0)
            {
                var normalizedSwapTime = 1 - (float)_swapFrames / _swapDuration;
                var prevPos = _charactersManager.GetWorldCoordinatesForPosition(_characterBeingSwapped.Character.CombatState.Position);
                var targetPos = _charactersManager.GetWorldCoordinatesForPosition(Positions.Frontline);
                _characterBeingSwapped.Character.GameObject.transform.position = Vector3.Lerp(prevPos, targetPos, normalizedSwapTime);
                return true;
            }

            _characterBeingSwapped.Character.CombatState.Position = Positions.Frontline;
            RefreshCharactersList();
            _isSwapping = false;
            return false;
        }

        private bool FrontlineNeedsReplacement()
        {
            if (!_charactersManager.HasEnemyInPosition(Positions.Frontline, out var frontliner))
                return true;
            if (frontliner == null)
                return true;
            var isDead = frontliner.Health.IsDead;
            if (!isDead)
                return false;
            var hasFinishedDeathAnimation = frontliner.Controller.CurrentAction is null;
            return hasFinishedDeathAnimation;
        }
    }
}