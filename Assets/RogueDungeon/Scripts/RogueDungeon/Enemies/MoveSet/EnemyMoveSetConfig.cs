using System.Collections.Generic;
using System.Linq;
using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyMoveSetConfig : MoveSetConfig
    {
        [SerializeField] private EnemyMoveConfig[] _moves;
        public override IEnumerable<MoveConfig> Moves => ExtendsConfigs.SelectMany(n => n.Moves).Concat(_moves);
    }
}