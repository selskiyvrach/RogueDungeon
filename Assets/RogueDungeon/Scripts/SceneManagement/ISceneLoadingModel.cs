namespace RogueDungeon.SceneManagement
{
    public interface ISceneLoadingModel : ILoadingModel
    {
        void Load(string name);
    }
}