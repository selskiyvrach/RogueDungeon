using RogueDungeon.StateMachine;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class HasWalkInputCondition : ICondition
    {
        public bool IsMet() => 
            Input.GetKey(KeyCode.W);
    }
}