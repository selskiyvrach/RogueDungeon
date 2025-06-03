using Game.Features.Items.Domain;
using Game.Features.Items.Infrastructure.Repository;
using Libs.Fsm;
using Libs.Movesets;
using Libs.Utils.Zenject;
using Zenject;
using IMoveSetConfig = Libs.Movesets.IMoveSetConfig;

namespace Game.Features.Items.Infrastructure.Factory
{
    public class ItemMoveSetFactory : IFactory<IItem, StateMachine>
    {
        private readonly DiContainer _container;
        private readonly MoveSetFactory _moveSetFactory;
        private readonly IItemConfigsRepository _configsRepository;

        public ItemMoveSetFactory(DiContainer container, MoveSetFactory moveSetFactory, IItemConfigsRepository configsRepository)
        {
            _container = container;
            _moveSetFactory = moveSetFactory;
            _configsRepository = configsRepository;
        }

        public StateMachine Create(IItem item)
        {
            var config = _configsRepository.GetItemConfig(item.TypeId);
            var container = _container.CreateSubContainer();
            container.BindAllInterfacesAndBaseClasses(item);
            _moveSetFactory.Container = container;
            return _moveSetFactory.Create((IMoveSetConfig)config);
        }
    }
}