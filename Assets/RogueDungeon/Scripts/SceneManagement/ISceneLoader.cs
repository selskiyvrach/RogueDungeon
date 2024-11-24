namespace RogueDungeon.SceneManagement
{
    public interface ISceneLoader
    {
        void Load<T>() where T : Scene, new();
    }
}