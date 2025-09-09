using Game.Features.Player.Domain.Hands;
using Game.Libs.Items;

namespace Game.Features.Player.App.Presenters
{
    public class HandPresenter
    {
        private readonly HandBehaviour _hand;
        private readonly IHandheldItemView _view;
        private readonly IItemConfigsRepository _configsRepository;

        public HandPresenter(HandBehaviour hand, IHandheldItemView view, IItemConfigsRepository configsRepository)
        {
            _hand = hand;
            _view = view;
            _configsRepository = configsRepository;
            _hand.OnCurrentItemChanged += UpdateView;
            UpdateView();
        }

        private void UpdateView()
        {
            if(_hand.CurrentItem is not {} item)
                _view.Hide();
            else
                _view.Show(_configsRepository.GetHandheldItemViewConfig(item.TypeId).Sprite);
        }
    }
}