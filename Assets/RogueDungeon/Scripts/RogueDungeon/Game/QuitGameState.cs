using Common.Game;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RogueDungeon.Game
{
    public class QuitGameState : IGameState
    {
        public void Enter()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
#else 
            Application.Quit();
#endif
        }
    }
}