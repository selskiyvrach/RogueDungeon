using System;
using UnityEngine;

namespace Common.MoveSets
{
    [Serializable]
    public class TransitionPicker
    {
        [field: SerializeField] public string MoveId { get; private set; }
        [field: SerializeField] public bool CanInterrupt { get; private set; }
    }

    public class Transition
    {
        public Move Move { get; }
        public bool CanInterrupt { get; }

        public Transition(Move move, bool canInterrupt)
        {
            Move = move;
            CanInterrupt = canInterrupt;
        }
    }
}