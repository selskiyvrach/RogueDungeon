using System;
using UnityEngine;

namespace Common.Animations
{
    [Serializable]
    public struct FrameEvent
    {
        [field: SerializeField] public int Frame { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}