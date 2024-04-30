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

        public void CreateCharacter(string configName, Positions positions)
        {
            if (HasEnemyInPosition(positions, out Character enemy))
            {
                Debug.LogError($"'{positions}' position is already occupied");
                return;
            }

            var character = _characterFactory.Create(configName);
            if (character == null)
            {
                Debug.LogError("Character cannot be null");
                return;
            }
            
            character.CombatState.positions = positions;
            character.CombatState.SurroundingCharacters = this;
            character.CombatState.Surroundings = this;
            _characters.Add(character);
        }

        private bool HasEnemyInPosition(Positions positions, out Character character)
        {
            character = AllCharacters.FirstOrDefault(n => n.CombatState.positions == positions);
            return character != null;
        }

        public Character GetTargetForPosition(Positions positions) =>
            positions == Positions.Player
                ? AllCharacters.FirstOrDefault(n => n.CombatState.positions == Positions.Frontline)
                : AllCharacters.FirstOrDefault(n => n.CombatState.positions == Positions.Player);
        
        public void Tick()
        {
            foreach (var character in _characters)
                character.Tick();
        }

        public Vector3 GetWorldCoordinatesForPosition(Positions positions) => 
            _worldRelativePositionsConfig.GetRelativePosition(positions);
    }
}