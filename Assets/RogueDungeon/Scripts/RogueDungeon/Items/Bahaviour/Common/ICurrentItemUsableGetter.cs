using UniRx;

namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    public interface ICurrentItemUsableGetter
    {
        IReadOnlyReactiveProperty<bool> IsUsable { get; }
    }
}