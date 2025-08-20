using Game.Libs.UI;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IContainerView : IHoverable, IIDable
    {
        // set item (item, local position normalized)
        // get local pointer position normalized
        Vector2 ScreenPosToLocalPosNormalized(Vector2 point, Camera cam);
        Vector3 LocalPosNormalizedToWorldPos(Vector2 normalizedPoint);
    }
}