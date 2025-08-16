namespace Game.Libs.UI
{
    public interface IScreensService
    {
        void Show(IShowRequest request);
        void Hide(IHideRequest request);
    }
}