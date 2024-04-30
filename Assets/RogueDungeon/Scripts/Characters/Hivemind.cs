using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Utils;

namespace RogueDungeon.Characters
{
    public class Hivemind
    {
        private readonly List<AiCharacterController> _characters = new(3);
        private readonly CharactersManager _charactersManager;

        private int _currentCharacterIndex;
        private AiCharacterController _currentCharacter;
        private int _chillFrames;

        public Hivemind(CharactersManager charactersManager)
        {
            _charactersManager = charactersManager;
            RefreshCharactersList();
        }

        public void Tick()
        {
            if (_currentCharacter is { CurrentPattern: not null })
            {
                _currentCharacter.Tick();
                return;
            }

            if (_currentCharacterIndex == _characters.Count)
            {
                if (_chillFrames-- == 0) 
                    RefreshCharactersList();

                return;
            }

            _currentCharacter = _characters[_currentCharacterIndex++];
            _currentCharacter.StartNewPattern();
            _chillFrames += _currentCharacter.CurrentPattern.ChillFrames;
            
            if (_currentCharacterIndex == _characters.Count)
                _chillFrames /= _characters.Count;
        }

        private void RefreshCharactersList()
        {
            _characters.Clear();
            
            foreach (var character in _charactersManager.AllCharacters)
            {
                if (character.CombatState.Position != Positions.Player)
                    _characters.Add((AiCharacterController)character.Controller);
            }

            _characters.Shuffle();
            _currentCharacterIndex = 0;
        }
    }
}