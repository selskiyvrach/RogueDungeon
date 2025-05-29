using System;
using UnityEngine;

namespace InGameResources
{
    [Serializable]
    public class ResourceConfig
    {
        [field: SerializeField] public float Max { get; private set; } = 100;
    }
}