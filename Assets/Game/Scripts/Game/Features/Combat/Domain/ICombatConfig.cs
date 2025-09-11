using System.Collections.Generic;
using Game.Features.Combat.Domain.Enemies;
using Game.Libs.Combat;

namespace Game.Features.Combat.Domain
{
    public interface ICombatConfig
    {
        string LootId { get; }
        IEnumerable<(EnemyConfig config, EnemyPosition position)> SpawnInfos { get; }
    }
}