using System;
using UnityEngine;

namespace RogueDungeon.Actions
{
    [Serializable]
    public struct Keyframe
    {
        [field: SerializeField] public int Frame { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}