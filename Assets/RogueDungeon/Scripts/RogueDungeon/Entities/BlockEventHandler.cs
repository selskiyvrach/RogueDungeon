using Common.Events;
using RogueDungeon.Entities.Properties;
using RogueDungeon.Weapons;

namespace RogueDungeon.Entities
{
    public class BlockEventHandler : IEventHandler<BlockEvent>
    {
        private readonly Property<BlockState> _property;

        public BlockEventHandler(Property<BlockState> property) => 
            _property = property;

        public void HandleEvent(BlockEvent @event) =>
            _property.Value.CurrentlyBlockingWith = @event.BlockingWeapon;
    }
}