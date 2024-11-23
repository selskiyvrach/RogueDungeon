namespace RogueDungeon
{
    public interface IInitializable<T>
    {
        void Initialize(T viewModel);
    }
}