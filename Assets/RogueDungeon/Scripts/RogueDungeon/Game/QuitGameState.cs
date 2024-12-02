using System.Threading.Tasks;
using Common.Game;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RogueDungeon.Game
{
    public class QuitGameState : GameState
    {
        public override Task Enter()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
#else 
            Application.Quit();
#endif
            return Task.CompletedTask;
        }
    }
}