using System.Collections.Generic;
using Common.FSM;

namespace RogueDungeon.Player
{
    public interface IAvailableInteractionsProvider
    {
        List<IInteractable> Interactions { get; }
    }

    public interface IInteractable
    {
        public ICondition InteractionEnterCondition { get; }
        public IFinishableState InteractionState { get; }
    }
}