using System.Collections.Generic;
using RogueDungeon.Utils;

namespace RogueDungeon.Characters
{
    public class Hivemind
    {
        private readonly List<AiCharacterController> _characters = new(3);
        private readonly CharactersManager _charactersManager;

        private int _currentCharacterIndex;
        private AiCharacterController _currentCharacter;

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
                RefreshCharactersList();
                return;
            }

            _currentCharacter = _characters[_currentCharacterIndex++];
            _currentCharacter.StartNewPattern();
        }

        private void RefreshCharactersList()
        {
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