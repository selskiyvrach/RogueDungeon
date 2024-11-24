using RogueDungeon.Game;

namespace RogueDungeon.UI.Common
{
    public abstract class ViewModel : IViewModel
    {
        public virtual void Dispose()
        {
            
        }
    }

    public abstract class ViewModel<T> : ViewModel, IViewModel<T> where T : IModel
    {

    }
}