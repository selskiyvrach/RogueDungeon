using RogueDungeon.Configs;
using UnityEngine;

namespace RogueDungeon.Player
{
    [CreateAssetMenu(menuName = "Configs/Player", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig : Config
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}