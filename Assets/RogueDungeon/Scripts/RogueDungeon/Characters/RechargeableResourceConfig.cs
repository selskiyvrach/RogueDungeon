using System;
using UnityEngine;

namespace Characters
{
    [Serializable]
    public class RechargeableResourceConfig : ResourceConfig
    {
        [field: SerializeField] public float RechargeRate { get; private set; } = 20;
        [field: SerializeField] public float RechargeDelay { get; private set; } = 1;
    }
}