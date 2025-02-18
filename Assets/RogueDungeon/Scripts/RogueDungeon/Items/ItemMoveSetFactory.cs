using Common.MoveSets;
using Zenject;

namespace RogueDungeon.Items
{
    public class ItemMoveSetFactory : IFactory<ItemConfig, MoveSetBehaviour>
    {
        private readonly MoveSetFactory _moveSetFactory;
        
        public ItemMoveSetFactory(MoveSetFactory moveSetFactory) => 
            _moveSetFactory = moveSetFactory;

        public MoveSetBehaviour Create(ItemConfig param) => 
            _moveSetFactory.Create(param.MoveSetConfig);
    }
}