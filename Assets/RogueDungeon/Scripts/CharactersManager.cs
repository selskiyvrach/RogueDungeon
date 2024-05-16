using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Characters;
using RogueDungeon.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RogueDungeon
{
    public class CharactersManager : ISurroundingCharactersProvider, ISurroundingsProvider
    {
        private readonly CharacterScenePositions _worldRelativePositionsConfig;
        private readonly CharacterFactory _characterFactory;
        
        private readonly List<Character> _allEnemies = new();
        private readonly List<Character> _aliveEnemies = new();
        private readonly Hivemind _hivemind;
        private readonly GameObject _charactersRoot = new("Characters");

        public IReadOnlyList<Character> AllEnemies => _allEnemies;
        public IReadOnlyList<Character> AliveEnemies => _aliveEnemies;
        public Character Player { get; private set; }

        public CharactersManager(CharacterFactory characterFactory, CharacterScenePositions worldRelativePositionsConfig, GameObject parent)
        {
            _charactersRoot.transform.SetParent(parent.transform);
            _characterFactory = characterFactory;
            _worldRelativePositionsConfig = worldRelativePositionsConfig;
            _hivemind = new Hivemind(this);
        }

        public void CreateCharacter(string configName, Position? pos = null, IHealthDisplay healthDisplay = null)
        {
            pos ??= !HasCharacterInPosition(Position.Frontline, out var _) 
                ? Position.Frontline 
                : HasCharacterInPosition(Position.BacklineLeft, out var _) 
                    ? Position.BacklineRight 
                    : Position.BacklineLeft;
            var position = (Position)pos;
            
            if (HasCharacterInPosition(position, out Character existingChar))
            {
                Debug.LogError($"Cannot create character at pos '{position}' - position is already occupied");
                return;
            }

            var character = _characterFactory.Create(configName, healthDisplay);
            if (character == null)
            {
                Debug.LogError("Character cannot be null");
                return;
            }
            character.GameObject.transform.SetParent(_charactersRoot.transform, worldPositionStays: false);
            character.CombatState.Position = position;
            character.CombatState.SurroundingCharacters = this;
            character.CombatState.Surroundings = this;
            UpdateCharacterPosition(character);
            if (position == Position.Player)
                Player = character;
            else
            {
                _allEnemies.Add(character);
                _aliveEnemies.Add(character);
            }
        }

        private void UpdateCharacterPosition(Character character) => 
            character.GameObject.transform.localPosition = GetLocalPosForPosition(character.CombatState.Position);

        public bool HasCharacterInPosition(Position position, out Character character)
        {
            character = position == Position.Player
                ? Player
                : _allEnemies.FirstOrDefault(n => n.CombatState.Position == position);
            return character != null;
        }
        
        public Character GetCharacterAtPos(Position position) => 
            position == Position.Player
                ? Player
                : HasCharacterInPosition(position, out var character)
                    ? character
                    : null;

        public Character GetTargetForPosition(Position position) =>
            position == Position.Player
                ? GetCharacterAtPos(Position.Frontline)
                : Player;
        
        public void Tick()
        {
            Player.Controller.Tick();
            UpdateCharacterPosition(Player);
            
            // update enemies state after player's actions
            foreach (var character in AllEnemies) 
                character.Controller.Tick();

            // clear dead enemies with post-death actions finished
            for (var i = _allEnemies.Count - 1; i >= 0; i--)
                if (_allEnemies[i].Health.IsDead && _allEnemies[i].Controller.CurrentAction is null)
                    RemoveEnemy(i);
            
            // update alive enemies
            _aliveEnemies.Clear();
            _aliveEnemies.AddRange(_allEnemies.Where(n => !n.Health.IsDead));
            
            // tick hivemind
            _hivemind.Tick();
        }

        public Vector3 GetLocalPosForPosition(Position position) => 
            _worldRelativePositionsConfig.GetRelativePosition(position);

        private void RemoveEnemy(int index)
        {
            Object.Destroy(_allEnemies[index].GameObject);
            _allEnemies.RemoveAt(index);
        }
    }
}