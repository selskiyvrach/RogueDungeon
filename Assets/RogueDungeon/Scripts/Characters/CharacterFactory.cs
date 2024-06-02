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

        /// <param name="configName"></param>
        /// <param name="staminaBar"></param>
        /// <param name="healthBar"> Can set a custom one. E.g. for the player or a boss. Otherwise is taken from the prefab</param>
        [CanBeNull]
        public Character Create(string configName, IResourceDisplay healthBar = null, IResourceDisplay staminaBar = null)
        {
            var config = Resources.Load<CharacterConfig>("Configs/Characters/" + configName);
            if(config == null)
            {
                Debug.LogError($"No config with name '{configName}' found");
                return null;
            }
            var gameObject = Object.Instantiate(config.Prefab, _parent);
            var animator = gameObject.GetComponent<Animator>();
            healthBar ??= gameObject.GetComponentInChildren<IResourceDisplay>();
            var character = new Character(config, animator, gameObject, healthBar, staminaBar);

            character.Controller = config.CreateController(character);
            return character;
        }
    }
}