namespace Common.Game
{
    public class Game
    {
        private readonly IGameStateChanger _stateChanger;

        public Game(IGameStateChanger stateChanger) => 
            _stateChanger = stateChanger;

        public void Start() => 
            _stateChanger.EnterState<BootstrapState>();
    }
}