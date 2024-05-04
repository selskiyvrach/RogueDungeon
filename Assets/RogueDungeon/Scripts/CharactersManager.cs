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
        private readonly Hivemind _hivemind;

        public IReadOnlyList<Character> AllCharacters => _characters;

        public CharactersManager(CharacterFactory characterFactory, CharacterScenePositions worldRelativePositionsConfig)
        {
            _characterFactory = characterFactory;
            _worldRelativePositionsConfig = worldRelativePositionsConfig;
            _hivemind = new Hivemind(this);
        }

        public void CreateCharacter(string configName, Positions position)
        {
            if (HasEnemyInPosition(position, out Character enemy))
            {
                Debug.LogError($"'{position}' position is already occupied");
                return;
            }

            var character = _characterFactory.Create(configName);
            if (character == null)
            {
                Debug.LogError("Character cannot be null");
                return;
            }
            
            character.CombatState.Position = position;
            character.CombatState.SurroundingCharacters = this;
            character.CombatState.Surroundings = this;
            character.GameObject.transform.position = GetWorldCoordinatesForPosition(position);
            _characters.Add(character);
        }

        public bool HasEnemyInPosition(Positions positions, out Character character)
        {
            character = AllCharacters.FirstOrDefault(n => n.CombatState.Position == positions);
            return character != null;
        }

        public Character GetTargetForPosition(Positions positions) =>
            positions == Positions.Player
                ? AllCharacters.FirstOrDefault(n => n.CombatState.Position == Positions.Frontline)
                : AllCharacters.FirstOrDefault(n => n.CombatState.Position == Positions.Player);
        
        public void Tick()
        {
            _hivemind.Tick();
            _characters.FirstOrDefault(n => n.CombatState.Position == Positions.Player)?.Controller.Tick();
        }

        public Vector3 GetWorldCoordinatesForPosition(Positions positions) => 
            _worldRelativePositionsConfig.GetRelativePosition(positions);
    }
}