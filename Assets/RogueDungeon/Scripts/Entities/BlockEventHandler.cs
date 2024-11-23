using RogueDungeon.Entities.Properties;
using RogueDungeon.Services.Events;
using RogueDungeon.Weapons;

namespace RogueDungeon.Entities
{
    public class BlockEventHandler : IEventHandler<BlockEvent>
    {
        private readonly Property<BlockState> _property;

        public BlockEventHandler(Property<BlockState> property) => 
            _property = property;

        public void Handle(BlockEvent @event) =>
            _property.Value.CurrentlyBlockingWith = @event.BlockingWeapon;
    }
}