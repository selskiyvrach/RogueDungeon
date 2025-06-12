using System.Linq;
using Game.Features.Combat.Domain.Enemies;
using UnityEngine;

namespace Game.Features.Combat.Infrastructure
{
    public class EnemyConfigsRepository : ScriptableObject, IEnemyConfigsRepository
    {
        [SerializeField] private EnemyConfig[] _configs;
        
        public EnemyConfig Get(string id) => 
            _configs.First(n => n.Id == id);
    }
}