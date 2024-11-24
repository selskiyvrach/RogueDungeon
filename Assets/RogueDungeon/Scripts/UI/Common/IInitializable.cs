namespace RogueDungeon.UI.Common
{
    public interface IInitializable<in T> where T : IViewModel
    {
        void Initialize(T viewModel);
    }
}