namespace RogueDungeon.Services.Registries
{
    public interface IRegistryNode<T>
    {
        void AddChildren(params IRegistry<T>[] children);
        void RemoveChildren(params IRegistry<T>[] children);
    }
}