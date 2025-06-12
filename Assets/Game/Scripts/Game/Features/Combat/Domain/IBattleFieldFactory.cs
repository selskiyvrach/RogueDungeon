using UnityEngine;

namespace Game.Features.Combat.Domain
{
    public interface IBattleFieldFactory
    {
        Transform CreateBattleField(Vector2Int position, Vector2Int rotation);
    }
}