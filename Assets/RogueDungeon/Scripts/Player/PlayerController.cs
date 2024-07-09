using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private State[] _states;
        public State CurrentState { get; private set; }
        public int CurrFrame { get; private set; }
        public Command Command { get; private set; }
        
        public event Action OnStateChanged;

        private void Start() => 
            StartState(GetState("Idle"));
        
        private void StartState(State state)
        {
            var prevState = CurrentState;
            CurrentState = state;
            CurrFrame = 0;
            if(CurrentState != prevState)
                OnStateChanged?.Invoke();
        }

        private void Update()
        {
            if (TryGetCommand(out var command))
                Command = command;

            if(CurrFrame >= CurrentState.Frames)
                return;
            
            CurrFrame++;
            
            if (TryTransitionFromState(CurrentState)) 
                return;
            
            if(CurrFrame == CurrentState.Frames)
                Debug.LogError($"No valid transition found. State: {CurrentState.Name}, command: {Command}, frame: {CurrFrame}");
        }

        private bool TryTransitionFromState(State state)
        {
            foreach (var transition in state.Transitions)
            {
                if(CurrFrame < state.Frames && (transition.Condition & Condition.Finished) != 0)
                    continue;

                if (transition.Commands != 0 && (Command & transition.Commands) == 0)
                    continue;

                Command ^= transition.ConsumesCommands & Command;
                
                StartState(GetState(transition.State));
                return true;
            }

            return false;
        }

        private State GetState(string stateName) =>
            _states.FirstOrDefault(n => n.Name == stateName) ?? throw new Exception($"No state with name [{stateName}] has been found");

        private bool TryGetCommand(out Command command)
        {
            command = Command.None;
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
                command = Command.Attack;
        
            if (UnityEngine.Input.GetKeyDown(KeyCode.A)) 
                command = Command.DodgeLeft;
            
            if(UnityEngine.Input.GetKeyDown(KeyCode.D))
                command = Command.DodgeRight;
        
            if (UnityEngine.Input.GetMouseButton(1))
                command = Command.Block;

            return command != Command.None;
        }
    }

    [Flags]
    public enum Command
    {
        None = 0,
        Attack = 1,
        Block = 2,
        DodgeLeft = 4,
        DodgeRight = 8,
        Any = Attack | Block | DodgeLeft | DodgeRight,
    }
    
    [Flags]
    public enum Condition
    {
        Finished = 1,
        HasCommands = 2,
    }

    [Serializable]
    public class Transition
    {
        [field: SerializeField] public string State { get; private set; }
        [field: SerializeField] public Condition Condition { get; private set; } = Condition.Finished;
        [field: ShowIf("RequiresCommands"), SerializeField] public Command Commands { get; private set; } = Command.None;
        [field: SerializeField] public Command ConsumesCommands { get; private set; }
        private bool RequiresCommands() => (Condition & Condition.HasCommands) != 0;
    }
    
    [Serializable]
    public class State
    {
        [field: Title("@Name"), SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Frames { get; private set; }
        [field: SerializeField] public Transition[] Transitions { get; private set; }
    }
}