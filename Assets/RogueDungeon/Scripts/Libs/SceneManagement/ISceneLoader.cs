using System.Threading.Tasks;

namespace Libs.SceneManagement
{
    public interface ISceneLoader
    {
        Task Load<T>() where T : Scene, new();
    }
}