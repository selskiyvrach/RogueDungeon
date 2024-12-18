using UniRx;

namespace RogueDungeon.Items.Handling.Common
{
    public interface ICurrentHandheldItemProvider
    {
        IItemInfo ItemInfo { get; }
    }

    public interface ICurrentItemEnabledState
    {
        IReadOnlyReactiveProperty<bool> Enabled { get; }
        void SetEnabled(bool value);
    }
}