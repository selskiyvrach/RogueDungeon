using JetBrains.Annotations;
using RogueDungeon.Actions;
using RogueDungeon.Data;
using RogueDungeon.Health;
using UnityEngine;
using Animator = RogueDungeon.Animations.Animator;

namespace RogueDungeon.Characters
{
    public class CharacterFactory
    {
        private readonly Transform _parent;
        private readonly ActionFactory _characterFactory;

        public CharacterFactory(Transform parent, StandardValues standardValues)
        {
            _parent = parent;
            _characterFactory = new ActionFactory(standardValues);
        }

        [CanBeNull]
        public Character Create(string configName)
        {
            var config = Resources.Load<CharacterConfig>("Configs/Characters/" + configName);
            if(config == null)
            {
                Debug.LogError($"No config with name '{configName}' found");
                return null;
            }
            var gameObject = Object.Instantiate(config.Prefab, _parent);
            var animator = gameObject.GetComponent<Animator>();
            var healthDisplay = gameObject.GetComponent<HealthDisplay>();
            var character = new Character(config, animator, healthDisplay, gameObject);

            character.Controller = config.CreateController(character, _characterFactory);
            return character;
        }
    }
}