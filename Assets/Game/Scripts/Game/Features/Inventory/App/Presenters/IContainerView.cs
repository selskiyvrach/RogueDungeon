using Game.Libs.UI;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IContainerView : IHoverable, IResettable
    {
        void PlaceItem(IItemView item, Vector2 posNormalized);
        Vector2 ScreenPosToLocalPosNormalized(Vector2 point, Camera cam);
        Vector3 LocalPosNormalizedToWorldPos(Vector2 normalizedPoint);
    }
}