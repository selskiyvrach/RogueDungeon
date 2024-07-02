using UnityEngine;
using UnityEngine.Serialization;

namespace RogueDungeon.Weapons
{
    public class WeaponTest : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 20;
        [FormerlySerializedAs("_weapon")] [SerializeField] private PlayerController _playerController;

        private void Update()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
        
        private void OnGUI()
        {
            var labelRect = new Rect(100, 100, 200, 20);
            var style = new GUIStyle
            {
                fontSize = 100,
            };
            var text = $"[{_playerController.CurrentState.Name}]: {_playerController.CurrFrame}/{_playerController.CurrentState.Frames}" +
                       $"\n" +
                       "[Command]: " + (_playerController.Command == null ? "null" : _playerController.Command.ToString());
            GUI.Label(labelRect, text, style);
        }
    }
}