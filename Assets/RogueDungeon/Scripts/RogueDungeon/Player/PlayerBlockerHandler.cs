namespace RogueDungeon.Player
{
    public class PlayerBlockerHandler : IBlocker
    {
        // private readonly stamina manager
        public bool HasUnabsorbedImpact { get; set; }
        public bool IsBlocking { get; set; }
        public void PerformBlock(float damage, out float damageAfterBlocking)
        {
            HasUnabsorbedImpact = true;
            damageAfterBlocking = 0;
        }
    }
}