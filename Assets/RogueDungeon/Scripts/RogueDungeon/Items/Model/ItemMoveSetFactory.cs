using Common.Fsm;
using Common.MoveSets;
using Common.UtilsZenject;
using Zenject;

namespace RogueDungeon.Items.Model
{
    public class ItemMoveSetFactory
    {
        private readonly DiContainer _container;
        private readonly MoveSetFactory _moveSetFactory;

        public ItemMoveSetFactory(DiContainer container, MoveSetFactory moveSetFactory)
        {
            _container = container;
            _moveSetFactory = moveSetFactory;
        }

        public StateMachine Create(IItem item)
        {
            var container = _container.CreateSubContainer();
            container.BindAllInterfacesAndBaseClasses(item);
            _moveSetFactory.Container = container;
            return _moveSetFactory.Create(item.Config);
        }
    }
}