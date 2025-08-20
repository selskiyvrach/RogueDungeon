using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IProjectionView
    {
        void SetSprite(Sprite sprite);
        void SetPosition(Vector3 worldPosition);
        void SetIsValid(bool isValid);
    }
}