using System.Linq;
using JetBrains.Annotations;
using RogueDungeon.Actions;
using RogueDungeon.Health;
using UnityEngine;

namespace RogueDungeon.Characters
{
    public class CharacterFactory
    {
        private readonly Transform _parent;

        public CharacterFactory(Transform parent) => 
            _parent = parent;

        [CanBeNull]
        public Character Create(string configName, Position position)
        {
            var config = Resources.Load<CharacterConfig>("Configs/Characters/" + configName);
            if(config == null)
            {
                Debug.LogError($"No config with name '{configName}' found");
                return null;
            }
            var gameObject = Object.Instantiate(config.Prefab, _parent);
            var animator = gameObject.GetComponent<RogueDungeon.Animations.Animator>();
            var healthDisplay = gameObject.GetComponent<HealthDisplay>();
            var character = new Character(config, animator, healthDisplay);
            
            var attackActions = config.AttackConfigs.Select(n => ActionFactory.Create(character, n.Id));
            var blockActions = config.BlockConfigs.Select(n => ActionFactory.Create(character, n.Id));
            var actions = config.ActionConfigs.Select(n => ActionFactory.Create(character, n.Name));
            foreach (var action in attackActions.Concat(blockActions).Concat(actions))
                character.Actions.Add(action.Command, action);

            character.Controller = config.Controller == "Player"
                ? new KeyboardCharacterController(character)
                : new PatternCharacterController(character, 45, new []{"AttackLeft", "AttackRight", "AttackCenter"});

            character.CombatState.Position = position; 
            return character;
        }
    }
}