namespace Game.Libs.UI
{
    public interface IHoverDisplayable
    {
        bool IsHovered { get; }
        void DisplayHovered(bool hovered);
    }
}