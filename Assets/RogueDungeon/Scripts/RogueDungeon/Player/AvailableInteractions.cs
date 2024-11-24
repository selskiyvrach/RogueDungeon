using System.Collections.Generic;

namespace RogueDungeon.Player
{
    public class AvailableInteractions : IAvailableInteractionsProvider
    {
        public List<IInteractable> Interactions { get; }
    }
}