namespace Game.UI.Screens
{
    public abstract class Screen : Libs.UI.Screen
    {
        protected abstract DrawOrder DrawOrder { get; }
        public sealed override int SortingOrder => (int)DrawOrder;
    }
}