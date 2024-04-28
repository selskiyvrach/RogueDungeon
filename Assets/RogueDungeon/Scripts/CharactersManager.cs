using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon
{
    public class CharactersManager : ISurroundingCharactersProvider, ISurroundingsProvider
    {
        private readonly CharacterScenePositions _worldRelativePositionsConfig;
        private readonly CharacterFactory _characterFactory;
        private readonly List<Character> _characters = new();

        public IReadOnlyList<Character> AllCharacters => _characters;

        public CharactersManager(CharacterFactory characterFactory, CharacterScenePositions worldRelativePositionsConfig)
        {
            _characterFactory = characterFactory;
            _worldRelativePositionsConfig = worldRelativePositionsConfig;
        }

        public void CreateCharacter(string configName, Position position)
        {
            var character = _characterFactory.Create(configName);
            if (character == null)
            {
                Debug.LogError("Character cannot be null");
                return;
            }
            
            character.CombatState.Position = position;
            character.CombatState.SurroundingCharacters = this;
            character.CombatState.Surroundings = this;
            _characters.Add(character);
        }
        
        public Character GetTargetForPosition(Position position) =>
            position == Position.Player
                ? AllCharacters.FirstOrDefault(n => n.CombatState.Position == Position.Frontline)
                : AllCharacters.FirstOrDefault(n => n.CombatState.Position == Position.Player);
        
        public void Tick()
        {
            foreach (var character in _characters)
                character.Tick();
        }

        public Vector3 GetWorldCoordinatesForPosition(Position position) => 
            _worldRelativePositionsConfig.GetRelativePosition(position);
    }
}