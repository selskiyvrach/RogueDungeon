using System;
using UnityEngine;

namespace RogueDungeon.Characters
{
    [Serializable]
    public class AttackPattern 
    {
        [field: SerializeField] public Positions SuitableForPositions { get; private set; }
        [field: SerializeField] public int ChillFrames { get; private set; }
        [field: SerializeField] public string[] Attacks { get; private set; }
    }
}