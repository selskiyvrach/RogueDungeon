using RogueDungeon.Items.Model;
using RogueDungeon.Items.View;

namespace RogueDungeon.Scripts.RogueDungeon.Player.Presenter
{
    public class HandheldItemPresenter
    {
        private readonly HandHeldItemView _itemView;

        public HandheldItemPresenter(HandHeldItemView itemView) => 
            _itemView = itemView;

        public void Show(IHandheldItem item) => 
            _itemView.Show(item.Config.Sprite);

        public void Hide() => 
            _itemView.Hide();
    }
}