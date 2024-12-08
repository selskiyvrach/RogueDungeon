using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours
{
    public interface IPlayerMovementAnimationConfigsProvider
    {
        AnimationConfig DodgeLeft { get; }
        AnimationConfig DodgeRight { get; }
        AnimationConfig Idle { get; }
    }

    public class PlayerMovementAnimationConfigsProvider : ScriptableObject, IPlayerMovementAnimationConfigsProvider
    {
        [field: SerializeField] public AnimationConfig DodgeLeft { get; private set;}
        [field: SerializeField] public AnimationConfig DodgeRight { get; private set;}
        [field: SerializeField] public AnimationConfig Idle { get; private set;}
    }
}