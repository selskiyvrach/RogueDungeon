using RogueDungeon.Configs;
using UnityEngine;

namespace RogueDungeon.Player
{
    [CreateAssetMenu(menuName = "Configs/Player/PlayerConfig", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig : Config
    {
        [field: SerializeField] public PlayerGameObjectInstaller Prefab { get; private set; }
    }
}