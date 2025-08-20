namespace Game.Libs.UI
{
    public interface IHoverDisplayable
    {
        void DisplayHovered();
        void DisplayUnhovered();
    }

    public interface IBeingDraggedDisplayable
    {
        void DisplayBeingDragged();
        void DisplayNotBeingDragged();
    }
}