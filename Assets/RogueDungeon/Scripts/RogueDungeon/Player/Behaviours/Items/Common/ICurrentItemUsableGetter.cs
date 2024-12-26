using UniRx;

namespace RogueDungeon.Items.Behaviours.Common
{
    public interface ICurrentItemUsableGetter
    {
        IReadOnlyReactiveProperty<bool> IsUsable { get; }
    }
}