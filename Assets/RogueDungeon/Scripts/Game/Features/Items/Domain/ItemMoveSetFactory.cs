using Libs.Fsm;
using Libs.Movesets;
using Libs.Utils.Zenject;
using Zenject;

namespace Game.Features.Items.Domain
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