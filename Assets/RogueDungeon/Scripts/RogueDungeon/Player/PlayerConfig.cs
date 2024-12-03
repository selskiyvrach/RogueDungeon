using RogueDungeon.Configs;
using RogueDungeon.Parameters;
using UnityEngine;

namespace RogueDungeon.Player
{
    [CreateAssetMenu(menuName = "Configs/Player/PlayerConfig", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig : Config
    {
        [field: SerializeField] public PlayerGameObjectInstaller Prefab { get; private set; }
        [field: SerializeField] public CharacterParametersConfig CharacterParametersConfig { get; private set; }
        [field: SerializeField] public TimingsConfig TimingsConfig { get; private set; }
    }
}