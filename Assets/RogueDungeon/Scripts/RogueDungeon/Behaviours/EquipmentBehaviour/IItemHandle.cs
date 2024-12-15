namespace RogueDungeon.Behaviours.EquipmentBehaviour
{
    public interface IItemHandle
    {
        object Id { get; }
        void SetVisible(bool value);
        void SetEnabled(bool value);
    }
}