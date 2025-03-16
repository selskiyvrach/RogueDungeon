namespace RogueDungeon.Player
{
    public interface IBlocker
    {
        bool IsBlocking { get; set; }
        void PerformBlock(float damage, out float damageAfterBlocking);
    }
}