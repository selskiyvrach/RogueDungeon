using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public class RoomGameObject : MonoBehaviour
    {
        [field: SerializeField] public RectTransform LootArea { get; private set; }
    }
}