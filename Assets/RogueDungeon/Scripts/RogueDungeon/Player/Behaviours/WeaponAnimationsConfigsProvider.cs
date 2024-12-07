using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours
{
    public class WeaponAnimationsConfigsProvider : ScriptableObject, IWeaponAnimationsConfigsProvider
    {
        [field: SerializeField] public AnimationConfig AttackPrepare { get; private set; }
        [field: SerializeField] public AnimationConfig AttackExecute { get; private set; }
        [field: SerializeField] public AnimationConfig AttackFinish { get; private set; }
        [field: SerializeField] public AnimationConfig WeaponIdle { get; private set; }
    }
}