using System.Collections.Generic;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IPresentersRegistry
    {
        HashSet<ContainerPresenter> Containers { get; }
        HashSet<ItemPresenter> Items { get; }
        void Register(ContainerPresenter container);
        void Register(ItemPresenter item);
        void Unregister(ContainerPresenter container);
        void Unregister(ItemPresenter item);
    }
}