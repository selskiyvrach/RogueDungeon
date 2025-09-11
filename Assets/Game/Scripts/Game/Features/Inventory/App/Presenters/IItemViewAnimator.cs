using System;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemViewAnimator
    {
        event Action OnAnimationFinished;
        void PlayDropped();
    }
}