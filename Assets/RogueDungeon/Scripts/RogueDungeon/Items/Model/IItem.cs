using RogueDungeon.Items.Model.Configs;
using Zenject;

namespace RogueDungeon.Items.Model
{
    public interface IItem
    {
        int InstanceId { get; }
        ItemConfig Config { get; }
    }
}