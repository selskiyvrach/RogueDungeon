using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IGraphicRaycaster
    {
        IEnumerable<T> RaycastAll<T>(Vector2 screenPoint);
    }
}