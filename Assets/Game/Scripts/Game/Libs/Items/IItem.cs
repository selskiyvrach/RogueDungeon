namespace Game.Libs.Items
{
    public interface IItem
    {
        int InstanceId { get; }
        string TypeId { get; }
    }
}