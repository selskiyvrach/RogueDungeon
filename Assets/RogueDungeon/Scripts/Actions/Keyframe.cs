using System;
using RogueDungeon.Data.Stats;
using UnityEngine;

namespace RogueDungeon.Actions
{
    [Serializable]
    public struct Keyframe
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public StatConfig Frame { get; private set; }
    }
}