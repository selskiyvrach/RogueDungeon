using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Player.Model
{
    public class PlayerBlockerHandler : IBlocker
    {
        private readonly Player _player;

        public PlayerBlockerHandler(Player player) => 
            _player = player;

        public bool HasUnabsorbedImpact { get; set; }
        public IItem BlockingItem { get; set; }
        public bool IsBlocking { get; set; }

        public void PerformBlock(float damage, out float damageAfterBlocking)
        {
            HasUnabsorbedImpact = true;
            var staminaCost = damage * BlockingItem.BlockStaminaCostMultiplier;
            damageAfterBlocking = Mathf.Clamp(damage - _player.Stamina.Current / BlockingItem.BlockStaminaCostMultiplier, 0, float.PositiveInfinity);
            _player.Stamina.AddDelta(- staminaCost);
        }
    }
}