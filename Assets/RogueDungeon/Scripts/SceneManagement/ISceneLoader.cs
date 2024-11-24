using System.Threading.Tasks;

namespace RogueDungeon.SceneManagement
{
    public interface ISceneLoader
    {
        Task Load<T>() where T : Scene, new();
    }
}