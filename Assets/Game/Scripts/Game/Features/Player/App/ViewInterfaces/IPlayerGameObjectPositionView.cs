using UnityEngine;

namespace Game.Features.Player.App.ViewInterfaces
{
    public interface IPlayerGameObjectPositionView
    {
        void SetPosition(Vector2 position);
        void SetRotation(Vector2 rotation);
    }
}