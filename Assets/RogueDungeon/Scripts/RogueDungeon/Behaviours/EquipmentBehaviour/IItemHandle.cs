namespace RogueDungeon.Behaviours.EquipmentBehaviour
{
    public interface IItemHandle
    {
        object Id { get; }
        float SheathDuration { get; }
        float UnsheathDuration { get; }
        void SetVisible(bool value);
        void SetEnabled(bool value);
    }
}