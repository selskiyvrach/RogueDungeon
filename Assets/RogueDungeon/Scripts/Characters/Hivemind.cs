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
        private int _chillFrames;

        public Hivemind(CharactersManager charactersManager)
        {
            _charactersManager = charactersManager;
            RefreshCharactersList();
        }

        public void Tick()
        {
            foreach (var character in _characters) 
                character.Tick();
            
            // check for died characters
            // compete for the frontline
                // wait for death animation to finish and for swap animation to finish
                // start new swap animation
                    // duration frames
                    // animator
                    // add moving forward for normalized attack duration
                
            if (_currentCharacter is { CurrentPattern: not null })
                return;

            if (_currentCharacterIndex == _characters.Count)
            {
                if (_chillFrames-- == 0) 
                    RefreshCharactersList();

                return;
            }

            _currentCharacter = _characters[_currentCharacterIndex++];
            _currentCharacter.StartNewPattern();
            _chillFrames += (int)_currentCharacter.CurrentPattern.ChillFrames.GetValue();
            
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