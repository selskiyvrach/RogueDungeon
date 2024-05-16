using System;
using RogueDungeon.Characters;
using RogueDungeon.Input;
using RogueDungeon.Maze;
using RogueDungeon.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace RogueDungeon
{
    public class Game
    {
        private readonly GameUI _gameUI;

        private enum State
        {
            Exploration,
            Combat,
            Crossroad,
        }

        private readonly CharactersManager _charactersManager;
        private readonly MazeExplorer _mazeExplorer;
        private State _currentState;
        private bool _playerIsDead;
        
        public GameObject LogicRoot { get; }

        public Game(CharacterFactory characterFactory, CharacterScenePositions scenePositions, GameUI gameUI)
        {
            _gameUI = gameUI;
            LogicRoot = new GameObject("Root");
            _charactersManager = new CharactersManager(characterFactory, scenePositions, LogicRoot);
            _mazeExplorer = new MazeExplorer(this, new Maze.Maze(), LogicRoot.transform);
            CreateCharacter("test-player", Position.Player, _gameUI.PlayerHealthBar);
            SwitchState(State.Exploration);
        }

        public void CreateCharacter(string configName, Position? position = null, IHealthDisplay healthDisplay = null)
        {
            _charactersManager.CreateCharacter(configName, position, healthDisplay);
            UpdateGameState();
        }

        public void Tick()
        {
            _mazeExplorer.Tick();
            _charactersManager.Tick();
            
            if(_playerIsDead)
                return;
            
            UpdateGameState();
            
            if (_charactersManager.Player.Health.IsDead && _charactersManager.Player.Controller.CurrentAction is null)
            {
                var deathScreen = Object.Instantiate(Resources.Load<Canvas>("Prefabs/UI/Screens/DeathScreen"));
                deathScreen.GetComponentInChildren<Button>().onClick.AddListener(Restart);
                _playerIsDead = true;
            }
        }

        private void Restart() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void SwitchState(State state)
        {
            _currentState = state;
            switch (_currentState)
            {
                case State.Exploration:
                    Input.Input.SetModeState(Mode.Combat, true);
                    Input.Input.SetModeState(Mode.Walking, true);
                    Input.Input.SetModeState(Mode.Crossroad, false);
                    break;
                case State.Combat:
                    Input.Input.SetModeState(Mode.Combat, true);
                    Input.Input.SetModeState(Mode.Walking, false);
                    Input.Input.SetModeState(Mode.Crossroad, false);
                    break;
                case State.Crossroad:
                    Input.Input.SetModeState(Mode.Combat, false);
                    Input.Input.SetModeState(Mode.Walking, false);
                    Input.Input.SetModeState(Mode.Crossroad, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateGameState()
        {
            if (_charactersManager.AliveEnemies.Count > 0)
            {
                if(_currentState != State.Combat)
                    SwitchState(State.Combat);
                return;
            }

            var requiredState = _mazeExplorer.IsOnCrossroad ? State.Crossroad : State.Exploration;
            if(requiredState != _currentState)
                SwitchState(requiredState);
        }
    }
}