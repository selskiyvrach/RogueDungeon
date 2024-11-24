using UnityEngine;

namespace RogueDungeon.Game
{
    public class QuitGameState : IGameState
    {
        public void Enter() => 
            Application.Quit();
    }
}