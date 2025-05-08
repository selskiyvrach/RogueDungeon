using RogueDungeon.UI;
using UnityEngine;

namespace RogueDungeon.Levels
{
    public class RoomPresenter : MonoBehaviour
    {
        [field: SerializeField] public WorldCanvas LootArea { get; private set; }
    }
}