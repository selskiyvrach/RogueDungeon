using UnityEngine;

namespace Game.Features.Combat.Domain
{
    public interface ICombat : ICombatLifecycleEvents
    {
        string Id { get; }
        void Initiate(string id, Vector2Int position, Vector2Int rotation);
    }
}