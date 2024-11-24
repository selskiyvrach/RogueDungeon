using RogueDungeon.Services.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RogueDungeon.Services.UnityUtils
{
    [RequireComponent(typeof(Button))]
    public class TextButton : MonoBehaviour, ITextButton
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;

        public ICommand Command { get; set; }

        private void OnValidate() => 
            _button ??= GetComponent<Button>();

        private void Awake() => 
            _button.onClick.AddListener(() => Command?.Execute());

        public void SetText(string text) => 
            _text.text = text;
    }
}