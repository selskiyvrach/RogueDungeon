using System;
using UnityEngine;

namespace Characters
{
    [Serializable]
    public class ResourceConfig
    {
        [field: SerializeField] public float Max { get; private set; } = 100;
    }
}