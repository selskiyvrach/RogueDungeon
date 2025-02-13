using UnityEngine;
using Input = RogueDungeon.Characters.Input.Input;

namespace RogueDungeon.MoveSets
{
    public class PlayerMoveConfig : MoveConfig
    {
        [field: SerializeField] public Input RequiredInput { get; private set; }
    }
}