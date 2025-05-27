using UI;
using UnityEngine;

namespace Levels
{
    public class RoomGameObject : MonoBehaviour
    {
        [field: SerializeField] public WorldCanvas LootArea { get; private set; }
    }
}