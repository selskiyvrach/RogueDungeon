namespace Game.Libs.UI
{
    public interface IScreen
    {
        bool AcceptsShowRequest(IShowRequest request);
        bool AcceptsHideRequest(IHideRequest request);
        
        void Show(IShowRequest request);
        void Hide(IHideRequest request);
    }
}