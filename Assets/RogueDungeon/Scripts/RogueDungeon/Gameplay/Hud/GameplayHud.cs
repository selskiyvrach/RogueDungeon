using Common.UI.Bars;
using UnityEngine;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayHud : MonoBehaviour
    {
        [field: SerializeField] public Bar PlayerHealthBar { get; private set; }
        [field: SerializeField] public Bar PlayerStaminaBar { get; private set; }
    }
}