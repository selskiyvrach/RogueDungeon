using Common.UI;

namespace UI
{
    public abstract class Screen : Common.UI.Screen
    {
        private ScreensSorter _sorter;
        
        protected abstract DrawOrder DrawOrder { get; }

        protected override void OnValidate()
        {
            base.OnValidate();
            Canvas.sortingOrder = (int)DrawOrder;
        }

        protected virtual void Destroy() => 
            Destroy(gameObject);
    }
}