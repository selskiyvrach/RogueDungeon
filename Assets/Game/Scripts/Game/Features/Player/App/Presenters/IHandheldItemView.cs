using UnityEngine;

namespace Game.Features.Player.App.Presenters
{
    public interface IHandheldItemView
    {
        void Show(Sprite sprite);
        void Hide();
    }
}