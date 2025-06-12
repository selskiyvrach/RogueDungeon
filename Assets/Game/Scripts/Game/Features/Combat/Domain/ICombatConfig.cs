using System.Collections.Generic;
using Game.Features.Combat.Domain.Enemies;

namespace Game.Features.Combat.Domain
{
    public interface ICombatConfig
    {
        IEnumerable<(EnemyConfig config, EnemyPosition position)> SpawnInfos { get; }
    }
}