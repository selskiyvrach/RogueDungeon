using System;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.App.UseCases
{
    public class ShowInventoryOnPlayerDomainRequestUseCase : IInitializable
    {
        private readonly Player.Domain.Player _player;
        private bool _initialized;

        public ShowInventoryOnPlayerDomainRequestUseCase(Player.Domain.Player player) => 
            _player = player;

        public void Initialize()
        {
            if (_initialized)
                throw new Exception("Already initialized");
            _initialized = true;
            _player.OnShowInventoryRequested += () => Debug.LogError("ShowInventoryRequested");
        }
    }
}