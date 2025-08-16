using Game.Features.Player.Domain.Movesets.Movement;
using Game.Libs.UI;
using Libs.Fsm;
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
            _player.OnStateChanged += HandleStateChanged;

        private void HandleStateChanged(IState state)
        {
            switch (state)
            {
                case InventoryKeepOpenMove:
                    Assert.IsFalse(_isOpen);
                    _screensService.Show(new ShowInventoryRequest());
                    _isOpen = true;
                    break;
                case InventoryCloseMove:
                    Assert.IsTrue(_isOpen);
                    _screensService.Hide(new HideInventoryRequest());
                    _isOpen = false;
                    break;
            }
        }
    }
}