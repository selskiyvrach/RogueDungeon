namespace Game.Libs.UI
{
    public interface IScreensRegistry
    {
        void Register(IScreen screen);
        void Unregister(IScreen screen);
    }
}