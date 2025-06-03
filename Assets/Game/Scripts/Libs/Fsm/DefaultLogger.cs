namespace Libs.Fsm
{
    public class DefaultLogger : ILogger
    {
        public void Log(string message) => 
            UnityEngine.Debug.Log(message);
    }
}