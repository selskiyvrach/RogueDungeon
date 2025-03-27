using RogueDungeon.Items;

namespace RogueDungeon.Player.Model
{
    public interface IBlocker
    {
        IItem BlockingItem { get; }
        bool IsBlocking { get; set; }
        void PerformBlock(float damage, out float damageAfterBlocking);
    }
}