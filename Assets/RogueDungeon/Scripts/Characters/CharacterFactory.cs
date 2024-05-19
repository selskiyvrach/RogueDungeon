using JetBrains.Annotations;
using RogueDungeon.UI;
using UnityEngine;
using Animator = RogueDungeon.Animations.Animator;

namespace RogueDungeon.Characters
{
    public class CharacterFactory
    {
        private readonly Transform _parent;

        public CharacterFactory(Transform parent) =>
            _parent = parent;

        /// <param name="healthDisplay"> Can set a custom one. E.g. for the player or a boss. Otherwise is taken from the prefab</param>
        [CanBeNull]
        public Character Create(string configName, IHealthDisplay healthDisplay = null)
        {
            var config = Resources.Load<CharacterConfig>("Configs/Characters/" + configName);
            if(config == null)
            {
                Debug.LogError($"No config with name '{configName}' found");
                return null;
            }
            var gameObject = Object.Instantiate(config.Prefab, _parent);
            var animator = gameObject.GetComponent<Animator>();
            healthDisplay ??= gameObject.GetComponentInChildren<IHealthDisplay>();
            var character = new Character(config, animator, healthDisplay, gameObject);

            character.Controller = config.CreateController(character);
            return character;
        }
    }
}