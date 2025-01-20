using System.Collections.Generic;
using System.Linq;
using Common.Animations;
using Common.Behaviours;
using Common.Fsm;
using RogueDungeon.Fsm;
using UniRx;

namespace RogueDungeon.MoveSets
{
    internal class MoveSetBehaviour : StateMachineBehaviour
    {
        public MoveSetBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        protected override void ToStartState() => 
            To<MoveSetIdleState>();
    }

    public interface ITryTransitionToMoveGetter
    {
        bool TryTransitionTo(MoveId id);
    }

    internal interface IMoveSetMovesGetter
    {
        public IMove Get(MoveId id);
        public IEnumerable<IMove> All { get; }
    }
    
    internal class MoveSetInternalFacade : IBehaviourInternalFacade, ICurrentMoveIdGetter, ICurrentMoveIdSetter, IPendingMoveIdGetter, IPendingMoveIdSetter
    {
        private MoveId? _currentMoveId;
        private MoveId? _pendingMoveId;

        MoveId? ICurrentMoveIdGetter.CurrentMoveId => _currentMoveId;
        MoveId? ICurrentMoveIdSetter.CurrentMoveId
        {
            set => _currentMoveId = value;
        }

        MoveId? IPendingMoveIdGetter.PendingMoveId => _pendingMoveId;
        MoveId? IPendingMoveIdSetter.PendingMoveId
        {
            set => _pendingMoveId = value;
        }
    }

    internal interface IMove
    {
        MoveId Id { get; }
        string AnimationName {get;}
        float Duration { get; }
        IMove[] Transitions { get; }
    }

    public readonly struct MoveId
    {
        
    }

    public readonly struct Event
    {
        public string Name { get; }

        public Event(string name) => 
            Name = name;
    }

    public interface IMoveSetEventObservable
    {
        ISubject<Event> OnEvent { get; }
    }

    internal interface IMoveSetEventRaiser
    {
        void Raise(Event @event);
    }

    internal interface ICurrentMoveIdGetter
    {
        MoveId? CurrentMoveId { get; }
    }

    internal interface ICurrentMoveIdSetter
    {
        MoveId? CurrentMoveId { set; }
    }
    
    internal interface IPendingMoveIdGetter
    {
        MoveId? PendingMoveId { get; }
    }

    internal interface IPendingMoveIdSetter
    {
        MoveId? PendingMoveId { set; }
    }
    
    internal class MoveSetIdleState : IState
    {
        private readonly IMoveSetMovesGetter _moveSetMovesGetter;
        private readonly IPendingMoveIdSetter _pendingMoveIdSetter;
        private readonly ITryTransitionToMoveGetter _tryTransitionToMoveGetter;
        
        public void CheckTransitions(IStateChanger stateChanger)
        {
            var moveToTransitionTo = _moveSetMovesGetter.All.FirstOrDefault(n => _tryTransitionToMoveGetter.TryTransitionTo(n.Id));
            if(moveToTransitionTo is null)
                return;
            
            _pendingMoveIdSetter.PendingMoveId = moveToTransitionTo.Id;
            stateChanger.To<MoveExecutionState>();
        }
    }

    internal class MoveExecutionState : BoundToAnimationState
    {
        private readonly IMoveSetMovesGetter _moveSetMovesGetter;
        private readonly IPendingMoveIdGetter _pendingMoveIdGetter;
        private readonly IPendingMoveIdSetter _pendingMoveIdSetter;
        private readonly ICurrentMoveIdGetter _currentMoveIdGetter;
        private readonly ICurrentMoveIdSetter _currentMoveIdSetter;
        private readonly ITryTransitionToMoveGetter _tryTransitionToMoveGetter;
        private readonly IMoveSetEventRaiser _eventRaiser;

        private IMove _move;

        protected override AnimationData Animation => new (_move.AnimationName, _move.Duration);
        
        public MoveExecutionState(IAnimator animator, IMoveSetMovesGetter moveSetMovesGetter, IPendingMoveIdGetter pendingMoveIdGetter, IPendingMoveIdSetter pendingMoveIdSetter, ICurrentMoveIdSetter currentMoveIdSetter, ICurrentMoveIdGetter currentMoveIdGetter, ITryTransitionToMoveGetter tryTransitionToMoveGetter, IMoveSetEventRaiser eventRaiser) : base(animator)
        {
            _moveSetMovesGetter = moveSetMovesGetter;
            _pendingMoveIdGetter = pendingMoveIdGetter;
            _pendingMoveIdSetter = pendingMoveIdSetter;
            _currentMoveIdSetter = currentMoveIdSetter;
            _currentMoveIdGetter = currentMoveIdGetter;
            _tryTransitionToMoveGetter = tryTransitionToMoveGetter;
            _eventRaiser = eventRaiser;
        }

        public override void Enter()
        {
            _currentMoveIdSetter.CurrentMoveId = _pendingMoveIdGetter.PendingMoveId;
            _pendingMoveIdSetter.PendingMoveId = null;
            _move = _moveSetMovesGetter.Get((MoveId)_currentMoveIdGetter.CurrentMoveId);
            base.Enter();
        }

        protected override void OnEvent(string name) => 
            _eventRaiser.Raise(new Event(name));

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsFinished)
                return;

            var transitionTo = _move.Transitions.FirstOrDefault(n => _tryTransitionToMoveGetter.TryTransitionTo(n.Id));
            if (transitionTo == null)
            {
                stateChanger.To<MoveSetIdleState>();
                return;
            }

            _pendingMoveIdSetter.PendingMoveId = transitionTo.Id;
            stateChanger.To<MoveExecutionState>();
        }
    }
}