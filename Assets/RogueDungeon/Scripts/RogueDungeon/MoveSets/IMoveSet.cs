using System;
using System.Collections.Generic;
using Common.Animations;
using Common.Fsm;
using RogueDungeon.Fsm;

namespace RogueDungeon.MoveSets
{
    public interface IMoveSet
    {
        public IReadOnlyList<IMove> Elements { get; }
    }

    public class MoveExecutionState : BoundToAnimationState
    {
        private readonly IMove _move;
        private readonly IMoveInputReader _moveInputReader;
        protected override AnimationData Animation => new (_move.AnimationName, _move.Duration);

        public MoveExecutionState(IMove move, IAnimator animator) : base(animator) => 
            _move = move;

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsFinished)
                return;

            // i created a type-based state machine
            // but here it requires other way of identification for states
            
            for (var i = 0; i < _move.Transitions.Length; i++)
            {
                var move = _move.Transitions[i];
                if (move is not IInputDependableMove requiresInput)
                {
                }
            }
        }
    }

    public interface IMove
    {
        // can have expected animation callbacks
        // checks if they and only they are being risen
        // has handlers for the callbacks
        
        string AnimationName {get;}
        float Duration { get; }
        IMove[] Transitions { get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class EditorNameAttribute : Attribute
    {
        public string Name { get; }

        public EditorNameAttribute(string name) => 
            Name = name;
    }

    public interface IInputDependableMove
    {
        bool HasRequiredInput(IMoveInputReader inputReader);
        void ConsumeInput(IMoveInputReader inputReader);
    }

    public interface IMoveInputReader
    {
        bool TryConsume<T>() where T : IMoveInputDefinition;
    }

    public interface IMoveInputDefinition
    {
        
    }

    public interface IEnterPointMove
    {
        
    }

    public interface IExitPointMove
    {
        
    }
}