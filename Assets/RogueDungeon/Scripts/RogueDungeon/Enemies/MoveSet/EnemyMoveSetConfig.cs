using System.Collections.Generic;
using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Enemies.MoveSet
{
    
    
    // birth config 
    // idle config -> raises 'is idle' flag
    // death config 
    
    // attack move 1
    // attack move 2
    
    // enemy behaviour -> has attacks for position ? get random attack for position

    public class EnemyMoveSetConfig : MoveSetConfig
    {
        [SerializeField] private EnemyMoveConfig[] _moves;
        public override IEnumerable<MoveConfig> Moves => _moves;
    }
}