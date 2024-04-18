using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Health;
using UnityEngine;

namespace RogueDungeon.Characters
{
    public static class CharacterFactory
    {
        public static Character Create(CharacterConfig config, Transform parent)
        {
            var gameObject = Object.Instantiate(config.Prefab, parent);
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

            return character;
        }
    }
}