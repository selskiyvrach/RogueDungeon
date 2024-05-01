using System;
using RogueDungeon.Data.Stats;
using UnityEngine;

namespace RogueDungeon.Characters
{
    [Serializable]
    public class AttackPattern 
    {
        [field: SerializeField] public Positions SuitableForPositions { get; private set; }
        [field: SerializeField] public StatConfig ChillFrames { get; private set; }
        [field: SerializeField] public string[] Attacks { get; private set; }
    }
}