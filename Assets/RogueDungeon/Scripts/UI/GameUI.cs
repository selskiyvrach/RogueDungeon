using UnityEngine;

namespace RogueDungeon.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private ResourceBar _playerHealthBar;
        [SerializeField] private ResourceBar _playerStaminaBar;

        public IResourceDisplay PlayerStaminaBar => _playerStaminaBar;
        public IResourceDisplay PlayerHealthBar => _playerHealthBar;
    }
}