using RogueDungeon.Gameplay.States;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay.Weapons
{
    public class WeaponBehaviour : IInteractable
    {
        public ICondition InteractionEnterCondition { get; }
        public IFinishableState InteractionState { get; }

        public WeaponBehaviour(ICondition interactionEnterCondition, IFinishableState interactionState)
        {
            InteractionEnterCondition = interactionEnterCondition;
            InteractionState = interactionState;
        }
        
        
    }
}