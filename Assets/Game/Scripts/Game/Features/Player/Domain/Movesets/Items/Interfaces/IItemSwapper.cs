using Game.Libs.Items;

namespace Game.Features.Player.Domain.Movesets.Items.Interfaces
{
    public interface IItemSwapper
    {
        IHandheldItem CurrentItem { get; set; }
        IHandheldItem IntendedItem { get; set; }        
    }
}