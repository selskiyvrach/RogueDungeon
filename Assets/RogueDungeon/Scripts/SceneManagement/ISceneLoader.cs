using System;

namespace RogueDungeon.SceneManagement
{
    public interface ISceneLoader
    {
        void LoadScene<T>(Action callback = null) where T : Scene, new();
    }
}