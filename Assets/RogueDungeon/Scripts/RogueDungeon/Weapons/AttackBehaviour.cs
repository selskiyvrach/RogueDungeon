using System;
using System.Collections.Generic;
using Common.DotNetUtils;
using Zenject;
using Behaviour = Common.Behaviours.Behaviour;

namespace RogueDungeon.Weapons
{
    internal interface IStatesFactory
    {
        T Create<T>() where T : IState;
    }

    internal class StatesFactoryWithCache
    {
        private readonly DiContainer _diContainer;
        private readonly List<IState> _cachedStates = new();

        public StatesFactoryWithCache(DiContainer diContainer) => 
            _diContainer = diContainer;

        public T Create<T>() where T : IState => 
            _cachedStates.Get<T>() ?? _cachedStates.With(_diContainer.Instantiate<T>()).Get<T>();
    }

    internal class WeaponBehaviour : Behaviour, IStateChanger
    {
        private readonly IStatesFactory _statesFactory;
        private readonly HashSet<IState> _transitionsHistory = new();
        private IState _currentState;

        public WeaponBehaviour(IStatesFactory statesFactory) => 
            _statesFactory = statesFactory;

        public override void Enable()
        {
            base.Enable();
            To<IdleState>();
        }

        public override void Tick(float timeDelta)
        {
            _currentState.Tick(timeDelta);
            _currentState.CheckTransitions(this);
        }

        public void To<T>() where T : IState
        {
            _transitionsHistory.Clear();
            _currentState.Exit();
            _currentState = _statesFactory.Create<T>();
            if (!_transitionsHistory.Add(_currentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            _currentState.Enter();
            _currentState.CheckTransitions(this);
        }
    }

    public enum Animation
    {
        None,
        Idle,
        PrepareAttackToBottomLeft,
        FinishFromBottomLeftAttack,
        FinishFromBottomRightAttack
    }

    public struct PlayOptions
    {
        public readonly Animation Type;
        public readonly float Duration;

        public PlayOptions(Animation type, float duration)
        {
            Type = type;
            Duration = duration;
        }
    }

    public interface IAnimator
    {
        void Play(PlayOptions playOptions);
    }

    public enum Duration
    {
        None,
        Idle,
        AttackIdleTransition,
        Attack,
        AttackAttackTransition,
    }

    public enum Input
    {
        None,
        Attack,
    }

    public interface IInput
    {
        bool TryConsume(Input input);
    }

    public interface IControlState
    {
        bool Is(AbleTo ableTo);
    }

    public enum AbleTo
    {
        None,
        StartAttack,
        TransitionToAttackExecutionState,
    }

    public interface IDurations
    {
        float Get(Duration type);
    }

    internal interface IState
    {
        void Enter();
        void Exit();
        void Tick(float timeDelta);
        void CheckTransitions(IStateChanger stateChanger);
    }

    internal interface IStateChanger
    {
        void To<T>() where T : IState;
    }

    internal interface IComboCounter
    {
        int Count { get; set; }
    }
    
    public enum AttackDirection
    {
        None,
        BottomLeft,
        BottomRight,
    }

    internal interface IComboInfo
    {
        AttackDirection[] Directions { get; }
    }

    internal abstract class State : IState
    {
        public  abstract void Enter();
        public virtual void Exit()
        {
        }

        public virtual void Tick(float timeDelta)
        {
        }

        public abstract void CheckTransitions(IStateChanger stateChanger);
    }

    internal abstract class TimerState : State
    {
        private float _timePassed;
        protected abstract float Duration { get; }
        public override void Enter() => 
            _timePassed = 0;
        protected bool IsTimerOff => _timePassed >= Duration;
        public override void Tick(float timeDelta) => 
            _timePassed += timeDelta;
    }

    internal abstract class TiedToAnimationState : TimerState
    {
        private readonly IAnimator _animator;
        private readonly IDurations _durations;
        private readonly Duration _duration;

        protected abstract Animation Animation { get; }
        protected override float Duration => _durations.Get(_duration);

        protected TiedToAnimationState(IAnimator animator, IDurations durations, Duration duration)
        {
            _animator = animator;
            _durations = durations;
            _duration = duration;
        }

        public override void Enter()
        {
            base.Enter();
            _animator.Play(new PlayOptions(Animation, Duration));
        }
    }

    internal class IdleState : State
    {
        private readonly IAnimator _animator;
        private readonly IDurations _durations;
        private readonly IInput _input;
        private readonly IControlState _controlState;
        
        public override void Enter() => 
            _animator.Play(new PlayOptions(Animation.Idle, _durations.Get(Duration.Idle)));

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(_controlState.Is(AbleTo.StartAttack) && _input.TryConsume(Input.Attack))
                stateChanger.To<AttackPrepareState>();
        }
    }
    
    internal class AttackPrepareState : TiedToAnimationState
    {
        private readonly IControlState _controlState;
        
        protected override Animation Animation => Animation.PrepareAttackToBottomLeft;

        public AttackPrepareState(IAnimator animator, IDurations durations) : 
            base(animator, durations, Weapons.Duration.AttackIdleTransition)
        {
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsTimerOff)
                return;
            if(_controlState.Is(AbleTo.TransitionToAttackExecutionState))
                stateChanger.To<AttackExecutionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }

    internal class AttackFinishState : TiedToAnimationState
    {
        private readonly IComboInfo _comboInfo;
        private readonly IComboCounter _comboCounter;
        protected override Animation Animation => _comboInfo.Directions[_comboCounter.Count] switch
        {
            AttackDirection.BottomLeft => Animation.FinishFromBottomLeftAttack,
            AttackDirection.BottomRight => Animation.FinishFromBottomRightAttack,
            _ => throw new ArgumentOutOfRangeException()
        };

        public AttackFinishState(IAnimator animator, IDurations durations, IComboInfo comboInfo, IComboCounter comboCounter) 
            : base(animator, durations, Weapons.Duration.AttackIdleTransition)
        {
            _comboInfo = comboInfo;
            _comboCounter = comboCounter;
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            throw new NotImplementedException();
        }
    }

    internal class AttackExecutionState : TiedToAnimationState
    {
        protected override Animation Animation { get; }

        public AttackExecutionState(IAnimator animator, IDurations durations, Duration duration) : base(animator, durations, duration)
        {
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            throw new NotImplementedException();
        }
    }
}