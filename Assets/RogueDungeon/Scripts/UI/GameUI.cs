using UnityEngine;

namespace RogueDungeon.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _playerHealthBar;
        
        public IHealthDisplay PlayerHealthBar => _playerHealthBar;
    }
}