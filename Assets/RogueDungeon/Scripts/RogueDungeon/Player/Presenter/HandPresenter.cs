using Player.Model.Behaviours.Hands;
using RogueDungeon.Items.Model;

namespace RogueDungeon.Scripts.RogueDungeon.Player.Presenter
{
    public class HandPresenter
    {
        private readonly HandBehaviour _playerHand;
        private readonly HandheldMapPresenter _mapPresenter;
        private readonly HandheldItemPresenter _itemPresenter;
        
        private IItem _currentItem;
        
        public HandPresenter(HandBehaviour playerHand, HandheldMapPresenter mapPresenter, HandheldItemPresenter itemPresenter)
        {
            _mapPresenter = mapPresenter;
            _itemPresenter = itemPresenter;
            
            _playerHand = playerHand;
            _playerHand.OnCurrentItemChanged += UpdateView;
        }

        private void UpdateView()
        {
            switch (_playerHand.CurrentItem)
            {
                case null:
                {
                    switch (_currentItem)
                    {
                        case Map:
                            _mapPresenter.Hide();
                            break;
                        case Item:
                            _itemPresenter.Hide();
                            break;
                    }
                    _currentItem = null;
                    break;
                }
                case Map:
                    _mapPresenter.Show();
                    break;
                default:
                    _itemPresenter.Show(_playerHand.CurrentItem);
                    break;
            }

            _currentItem = _playerHand.CurrentItem;
        }
    }
}