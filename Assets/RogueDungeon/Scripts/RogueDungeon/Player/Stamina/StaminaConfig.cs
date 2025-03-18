using System;
using UnityEngine;

namespace RogueDungeon.Player.Stamina
{
    [Serializable]
    public class StaminaConfig
    {
        [field: SerializeField] public float Max { get; private set; } = 100;
        [field: SerializeField] public float RechargeRate { get; private set; } = 20;
        [field: SerializeField] public float RechargeDelay { get; private set; } = 1;
    }
}