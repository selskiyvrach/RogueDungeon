namespace Game.Features.Items.Domain
{
    public interface IItem
    {
        int InstanceId { get; }
        string TypeId { get; }
    }
}