using Common.Animations;
using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Items
{
    public class Item
    {
        public readonly ItemConfig Config;

        public Item(ItemConfig config) => 
            Config = config;
    }

    public class WeaponMoveConfig : MoveConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
    }

    public class WeaponMove : Move
    {
        private readonly WeaponMoveConfig _config;

        protected WeaponMove( WeaponMoveConfig config, IAnimator animator) : base(config, animator)
        {
            _config = config;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            // _mediator.MediateAttack(_config);
        }
    }
}