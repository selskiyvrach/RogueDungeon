namespace RogueDungeon.Player.Model
{
    public interface IBlocker
    {
        bool IsBlocking { get; set; }
        void PerformBlock(float damage, out float damageAfterBlocking);
    }
}