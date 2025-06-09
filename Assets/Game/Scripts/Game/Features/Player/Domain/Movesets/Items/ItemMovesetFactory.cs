using Game.Libs.Items;
using Libs.Fsm;
using Libs.Movesets;
using Libs.Utils.DotNet;
using Zenject;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class ItemMovesetFactory
    {
        private readonly MoveSetFactory _moveSetFactory;
        private readonly DiContainer _container;
        private readonly IItemConfigsRepository _configsRepository;
        private readonly IMoveIdToTypeConverter _converter;

        public ItemMovesetFactory(DiContainer container, IItemConfigsRepository configsRepository, MoveSetFactory moveSetFactory, IMoveIdToTypeConverter converter)
        {
            _container = container;
            _configsRepository = configsRepository;
            _moveSetFactory = moveSetFactory;
            _converter = converter;
        }

        public StateMachine Create(IItem item)
        {
            var container = _container.CreateSubContainer();
            
            foreach (var type in item.GetType().GetAllBaseTypesAndInterfaces()) 
                container.Bind(type).FromInstance(item).AsSingle();
            
            _moveSetFactory.Container = container;
            return _moveSetFactory.Create(_configsRepository.GetItemMovesetConfig(item.TypeId), _converter);
        }
    }
}