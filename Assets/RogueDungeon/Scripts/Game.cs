using System;
using RogueDungeon.Characters;
using RogueDungeon.Input;
using RogueDungeon.Maze;
using UnityEngine;

namespace RogueDungeon
{
    public class Game
    {
        private enum State
        {
            Exploration,
            Combat,
            Crossroad,
        }

        private readonly CharactersManager _charactersManager;
        private readonly MazeExplorer _mazeExplorer;
        private State _currentState;
        
        public GameObject LogicRoot { get; }

        public Game(CharacterFactory characterFactory, CharacterScenePositions scenePositions)
        {
            LogicRoot = new GameObject("Root");
            _charactersManager = new CharactersManager(characterFactory, scenePositions, LogicRoot);
            _mazeExplorer = new MazeExplorer(this, new Maze.Maze(new []
            {
                new Tile(new Vector2Int(0 ,0)), 
                new Tile(new Vector2Int(0, 1)), 
                new Tile(new Vector2Int(0, 2)),
                new Tile(new Vector2Int(1, 2)), 
                new Tile(new Vector2Int(-1, 2), new (string id, Position pos)[]
                {
                    ("test-skeleton-swordsman", Position.Frontline),
                    ("test-skeleton-swordsman", Position.BacklineRight),
                    ("test-skeleton-swordsman", Position.BacklineLeft),
                }), 
                new Tile(new Vector2Int(0, 3)), 
            }), LogicRoot.transform);
            CreateCharacter("test-player", Position.Player);
            SwitchState(State.Exploration);
        }

        public void CreateCharacter(string configName, Position position)
        {
            _charactersManager.CreateCharacter(configName, position);
            UpdateGameState();
        }

        public void Tick()
        {
            _mazeExplorer.Tick();
            _charactersManager.Tick();
            UpdateGameState();
        }

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
            if (_charactersManager.AliveEnemies.Count > 0 && _currentState != State.Combat)
                SwitchState(State.Combat);
            if (_charactersManager.AliveEnemies.Count == 0 && _currentState == State.Combat)
                SwitchState(State.Exploration);
            if(_mazeExplorer.IsOnCrossroad != (_currentState == State.Crossroad))
                SwitchState(_mazeExplorer.IsOnCrossroad ? State.Crossroad : State.Exploration);
        }
    }
}