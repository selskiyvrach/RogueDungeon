using System.Linq;
using Common.Mvvm.View;
using Common.UiCommons;
using UnityEngine;

namespace RogueDungeon.UI.MainMenu
{
    [RequireComponent(typeof(TextButtonList))]
    public class MainMenuView : View<IMainMenuViewModel>, IMainMenuView
    {
        [SerializeField, HideInInspector] private TextButtonList _buttonList;

        private void OnValidate() => 
            _buttonList = GetComponent<TextButtonList>();

        public override void Initialize(IMainMenuViewModel viewModel)
        {
            var buttons = viewModel.MenuItems.ToArray();
            _buttonList.SetActiveItemsCount(buttons.Length);
            for (var i = 0; i < _buttonList.Count; i++)
            {
                _buttonList[i].Command = buttons[i].Command;
                _buttonList[i].SetText(buttons[i].DisplayName);
            }
        }
    }
}