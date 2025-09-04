using Game.Libs.UI;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Inventory.App.UseCases
{
    public class ShowHideWorldInventoryUseCase : IInitializable
    {
        private readonly Player.Domain.Player _player;
        private readonly IScreensService _screensService;
        private bool _isOpen;

        public ShowHideWorldInventoryUseCase(Player.Domain.Player player, IScreensService screensService)
        {
            _player = player;
            _screensService = screensService;
        }

        public void Initialize() => 
            _player.OnShowInventoryRequested += HandleStateChanged;

        private void HandleStateChanged(bool show)
        {
            Assert.AreNotEqual(show, _isOpen);
            if (show)
                _screensService.Show(new ShowInventoryRequest());
            else
                _screensService.Hide(new HideInventoryRequest());
            _isOpen = show;
        }
    }
}