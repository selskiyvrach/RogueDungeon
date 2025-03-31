using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Common.UI
{
    public class UiElement : MonoBehaviour
    {
        [SerializeField, Optional] private ShowHideHandler _showHideHandler;
        
        protected void Construct(IUiElementViewModel viewModel)
        {
            if(viewModel is IHideableUiElement hideableUiElement)
                hideableUiElement.IsVisible.Subscribe(SetVisible).AddTo(gameObject);
        }

        private void SetVisible(bool b)
        {
            if(b)
                _showHideHandler?.Show();
            else
                _showHideHandler?.Hide();
        }
    }
}