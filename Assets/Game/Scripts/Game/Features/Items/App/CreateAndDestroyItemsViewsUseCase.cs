using Game.Features.Items.Domain;
using Game.Features.Items.Infrastructure;
using Game.Features.Items.Infrastructure.Repository;
using Game.Features.Items.Infrastructure.View;
using Game.Features.Player.Domain.Behaviours.Hands;
using Libs.Fsm;
using Zenject;

namespace Game.Features.Items.App
{
    // on hand item changed event
        // hand id, item id
        // two hand handlers anyway
        
    public class CreateAndDestroyItemsViewsUseCase
    {
        private readonly IFactory<ItemConfig, StateMachine> _moveSetFactory;
        private readonly IFactory<ItemConfig, HandHeldItemView> _viewFactory;
        private readonly IItemConfigsRepository _configsRepository;
        private readonly HandHeldItemView _itemView;
        private readonly HandheldMapView _mapView;
        
        private readonly HandBehaviour _hand;

        public CreateAndDestroyItemsViewsUseCase(HandBehaviour hand, IFactory<ItemConfig, StateMachine> moveSetFactory, IItemConfigsRepository configsRepository)
        {
            _hand = hand;
            _moveSetFactory = moveSetFactory;
            _configsRepository = configsRepository;
            _hand.OnCurrentItemChanged += HandleItemChanged;
        }

        private void HandleItemChanged()
        {
            DestroyViewAndPresenter();

            if (_hand.CurrentItem != null)
                CreateViewAndPresenter();
        }

        private void CreateViewAndPresenter()
        {
            var config = _configsRepository.GetItemConfig(_hand.CurrentItem.Id);
            var moveSet = _moveSetFactory.Create((ItemConfig)config);
            var view = _viewFactory.Create((ItemConfig)config);
        }

        private void DestroyViewAndPresenter()
        {
            
        }
    }
}