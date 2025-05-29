namespace UI
{
    public abstract class Screen : Common.UI.Screen
    {
        protected abstract DrawOrder DrawOrder { get; }
        public sealed override int SortingOrder => (int)DrawOrder;
    }
}