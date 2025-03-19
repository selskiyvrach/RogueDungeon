using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerBlockerHandler : IBlocker
    {
        private readonly Player _player;

        public PlayerBlockerHandler(Player player) => 
            _player = player;

        public bool HasUnabsorbedImpact { get; set; }
        public bool IsBlocking { get; set; }
        
        public void PerformBlock(float damage, out float damageAfterBlocking)
        {
            HasUnabsorbedImpact = true;
            damageAfterBlocking = Mathf.Clamp(damage - _player.Stamina.Current, 0, float.PositiveInfinity);
            _player.Stamina.AddDelta(- damage);
        }
    }
}