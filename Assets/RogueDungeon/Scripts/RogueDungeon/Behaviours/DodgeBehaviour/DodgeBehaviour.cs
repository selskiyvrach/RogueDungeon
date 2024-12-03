using System;
using Common.FSM;
using UnityEngine;
using Behaviour = Common.Behaviours.Behaviour;

namespace RogueDungeon.Behaviours.DodgeBehaviour
{
    public enum DodgeState
    {
        None,
        Left,
        Right
    }

    public interface IDodgeAnimationsPlayer
    {
        void PlayDodgeLeft(float duration);
        void PlayDodgeRight(float duration);
    }

    public class DummyDodgeAnimationsPlayer : IDodgeAnimationsPlayer
    {
        public void PlayDodgeLeft(float duration) { }
        public void PlayDodgeRight(float duration) { }
    }

    public interface IDodgeSoundsPlayer
    {
        void PlayDodgeSound();
    }

    public class DummyDodgeSoundsPlayer : IDodgeSoundsPlayer
    {
        public void PlayDodgeSound() { }
    }

    public interface IDodgeParametersProvider
    {
        float GetDuration();
    }

    public class DummyDodgeParametersProvider : IDodgeParametersProvider
    {
        private readonly float _value;

        public DummyDodgeParametersProvider(float value) => 
            _value = value;

        public float GetDuration() => _value;
    }

    public interface IDodgeInputProvider
    {
        bool HasDodgeRightInput();
        bool HasDodgeLeftInput();
    }

    public class DodgeInputProvider : IDodgeInputProvider
    {
        private readonly KeyCode _dodgeRightKeyCode;
        private readonly KeyCode _dodgeLeftKeyCode;

        public DodgeInputProvider(KeyCode dodgeRightKeyCode, KeyCode dodgeLeftKeyCode)
        {
            _dodgeRightKeyCode = dodgeRightKeyCode;
            _dodgeLeftKeyCode = dodgeLeftKeyCode;
        }

        public bool HasDodgeRightInput() => 
            Input.GetKeyDown(_dodgeRightKeyCode);

        public bool HasDodgeLeftInput() => 
            Input.GetKeyDown(_dodgeLeftKeyCode);
    }

    public interface IDodgeMediator
    {
        bool CanStartDodge();
        void SetDodgeState(DodgeState dodgeState);
    }

    public class DummyDodgeMediator : IDodgeMediator
    {
        private Func<bool> _canStartChecker;
        private Action<DodgeState> _dodgeStateSetter;
        
        public bool CanStartDodge() => 
            _canStartChecker.Invoke();

        public void SetDodgeState(DodgeState dodgeState) => 
            _dodgeStateSetter.Invoke(dodgeState);
    }

    public class DodgeBehaviour : Behaviour
    {
        private readonly IDodgeMediator _mediator;
        private readonly IDodgeAnimationsPlayer _animationsPlayer;
        private readonly IDodgeSoundsPlayer _soundsPlayer;
        private readonly IDodgeParametersProvider _parametersProvider;
        private readonly IDodgeInputProvider _inputProvider;
        private readonly StateMachine _stateMachine;

        public DodgeBehaviour(
            IDodgeMediator mediator, 
            IDodgeAnimationsPlayer animationsPlayer, 
            IDodgeSoundsPlayer soundsPlayer, 
            IDodgeParametersProvider parametersProvider, 
            IDodgeInputProvider inputProvider)
        {
            _mediator = mediator;
            _animationsPlayer = animationsPlayer;
            _soundsPlayer = soundsPlayer;
            _parametersProvider = parametersProvider;
            _inputProvider = inputProvider;
            var dodgeIdle = new State().OnEnter(() => _mediator.SetDodgeState(DodgeState.None));
            
            var dodgeRight = new TimerState(_parametersProvider.GetDuration)
                .OnEnter(() => _mediator.SetDodgeState(DodgeState.Right))
                .OnEnter(() => _animationsPlayer.PlayDodgeRight(_parametersProvider.GetDuration()))
                .OnEnter(_soundsPlayer.PlayDodgeSound);

            var dodgeLeft = new TimerState(_parametersProvider.GetDuration)
                .OnEnter(() => _mediator.SetDodgeState(DodgeState.Left))
                .OnEnter(() => _animationsPlayer.PlayDodgeLeft(_parametersProvider.GetDuration()))
                .OnEnter(_soundsPlayer.PlayDodgeSound);

            var fsmBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            var canStartDodge = new If(_mediator.CanStartDodge);
            fsmBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(canStartDodge, new If(_inputProvider.HasDodgeRightInput)));
            fsmBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(canStartDodge, new If(_inputProvider.HasDodgeLeftInput)));
            fsmBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            fsmBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
            _stateMachine = fsmBuilder.Build();
        }

        public override void Enable()
        {
            base.Enable();
            _mediator.SetDodgeState(DodgeState.None);
            _stateMachine.Run();
        }

        public override void Disable()
        {
            base.Disable();
            _mediator.SetDodgeState(DodgeState.None);
            _stateMachine.Stop();
        }

        public override void Tick() => 
            _stateMachine.Tick();
    }
}