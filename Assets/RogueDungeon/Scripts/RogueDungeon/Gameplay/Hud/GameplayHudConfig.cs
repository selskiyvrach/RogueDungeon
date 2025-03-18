using UnityEngine;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayHudConfig : ScriptableObject
    {
        [field: SerializeField] public GameplayHudInstaller InstallerPrefab { get; private set; }
    }
}