using System.Linq;
using Common.Mvvm.View;
using Common.UI.Commons;
using UnityEngine;
using Zenject;

namespace RogueDungeon.UI.MainMenu
{
    [RequireComponent(typeof(TextButtonList))]
    public class MainMenuView : View<IMainMenuViewModel>, IMainMenuView
    {
        [SerializeField, HideInInspector] private TextButtonList _buttonList;
        private IMainMenuViewModel _viewModel;

        private void OnValidate() => 
            _buttonList = GetComponent<TextButtonList>();

        [Inject]
        public override void Construct(IMainMenuViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel = viewModel;
        }

        private void Start()
        {
            var buttons = _viewModel.MenuItems.ToArray();
            _buttonList.SetActiveItemsCount(buttons.Length);
            for (var i = 0; i < _buttonList.Count; i++)
            {
                _buttonList[i].Command = buttons[i].Command;
                _buttonList[i].SetText(buttons[i].DisplayName);
            }
        }
    }
}