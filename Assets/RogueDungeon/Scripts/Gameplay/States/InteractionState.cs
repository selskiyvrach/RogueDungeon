using System.Linq;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay.States
{
    public class InteractionState : IFinishableState, IEnterable, IExitable, ITickable, ICondition
    {
        private readonly IAvailableInteractionsProvider _interactionsProvider;
        private IInteractable _currentInteraction;
        private IInteractable _interactionCandidate;
        public bool IsFinished => _currentInteraction?.InteractionState.IsFinished ?? false;
        
        public InteractionState(IAvailableInteractionsProvider interactionsProvider) => 
            _interactionsProvider = interactionsProvider;

        public void Enter()
        {
            _currentInteraction = _interactionCandidate;
            (_currentInteraction?.InteractionState as IEnterable)?.Enter();
        }

        public void Exit()
        {
            (_currentInteraction?.InteractionState as IExitable)?.Exit();
            _currentInteraction = null;
            _interactionCandidate = null;
        }

        public void Tick() => 
            (_currentInteraction?.InteractionState as ITickable)?.Tick();

        bool ICondition.IsMet()
        {
            _interactionCandidate = _interactionsProvider.Interactions.FirstOrDefault(n => n.InteractionEnterCondition.IsMet());
            return _interactionCandidate != null;
        }
    }
}