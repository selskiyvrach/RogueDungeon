using System;
using System.Collections.Generic;
using System.Linq;
using Game.Features.Combat.Domain;
using Game.Features.Combat.Domain.Enemies;
using UnityEngine;

namespace Game.Features.Combat.Infrastructure
{
    public class CombatConfigsRepository : ScriptableObject, ICombatConfigsRepository
    {
        [Serializable]
        public class CombatConfig : ICombatConfig
        {
            public string Name;
            public EnemyConfig Left;
            public EnemyConfig Middle;
            public EnemyConfig Right;
            public IEnumerable<(EnemyConfig config, EnemyPosition position)> SpawnInfos => new[]
            {
                (Left, EnemyPosition.Left), 
                (Right, EnemyPosition.Right), 
                (Middle, EnemyPosition.Middle)
            }.Where(n => n.Item1 != null);
        }

        [SerializeField] private CombatConfig[] _configs;

        public ICombatConfig Get(string id) => 
            _configs.First(n => n.Name == id);
    }
}