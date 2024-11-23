using RogueDungeon.Weapons;

namespace RogueDungeon.Entities
{
    public class BlockState
    {
        public IWeapon CurrentlyBlockingWith { get; set; }

        public BlockState(IWeapon currentlyBlockingWith) => 
            CurrentlyBlockingWith = currentlyBlockingWith;
    }
}