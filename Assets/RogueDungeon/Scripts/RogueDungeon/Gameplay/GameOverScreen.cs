using System;
using RogueDungeon.UI;
using UnityEngine;
using Zenject;
using Screen = RogueDungeon.UI.Screen;

namespace Gameplay
{
    public class GameOverScreen : Screen
    {
        private Action _callback;

        protected override DrawOrder DrawOrder => DrawOrder.GameOver;

        [Inject]
        public void Construct(Action onAnyButtonClicked) => 
            _callback = onAnyButtonClicked;

        private void Update()
        {
            if (!Input.anyKeyDown) 
                return;
            
            _callback?.Invoke();
            Destroy();
        }
    }
}