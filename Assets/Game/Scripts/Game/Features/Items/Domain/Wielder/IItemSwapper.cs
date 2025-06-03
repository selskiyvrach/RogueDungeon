namespace Game.Features.Items.Domain.Wielder
{
    public interface IItemSwapper
    {
        IHandheldItem CurrentItem { get; set; }
        IHandheldItem IntendedItem { get; set; }        
    }
}