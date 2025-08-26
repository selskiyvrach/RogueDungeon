using System;
using UnityEngine;

namespace Game.Libs.UI
{
    public interface IHoverable
    {
        event Action OnHovered;
        event Action OnUnhovered;
    }
}