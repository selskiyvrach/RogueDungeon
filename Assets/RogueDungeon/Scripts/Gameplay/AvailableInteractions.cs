using System.Collections.Generic;
using RogueDungeon.Gameplay.States;

namespace RogueDungeon.Gameplay
{
    public class AvailableInteractions : IAvailableInteractionsProvider
    {
        public List<IInteractable> Interactions { get; }
    }
}