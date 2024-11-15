using RogueDungeon.Player;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Weapons
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