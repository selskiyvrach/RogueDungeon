using System;

namespace Game.Libs.UI
{
    public interface IHoverable : IRaycastable
    {
        event Action OnHovered;
        event Action OnUnhovered;
    }
}