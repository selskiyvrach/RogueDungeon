using System.Threading.Tasks;

namespace Common.SceneManagement
{
    public interface ISceneLoader
    {
        Task Load<T>() where T : Scene, new();
    }
}