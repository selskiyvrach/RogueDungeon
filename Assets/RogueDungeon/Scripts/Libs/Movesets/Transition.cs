using System;
using UnityEngine;

namespace Libs.Movesets
{
    [Serializable]
    public struct TransitionPicker
    {
        public TransitionPicker(string moveId, bool canInterrupt = false)
        {
            MoveId = moveId;
            CanInterrupt = canInterrupt;
        }

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