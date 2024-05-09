using System;
using RogueDungeon.Characters;
using RogueDungeon.Input;
using RogueDungeon.Maze;
using UnityEngine;

namespace RogueDungeon
{
    public class Game
    {
        public enum State
        {
            Exploration,
            Combat,
        }

        private readonly CharactersManager _charactersManager;
        private readonly Maze.Maze _maze;
        public State CurrentState { get; private set; }

        public Game(CharacterFactory characterFactory, CharacterScenePositions scenePositions)
        {
            _charactersManager = new CharactersManager(characterFactory, scenePositions);
            _maze = new Maze.Maze(this, new []
            {
                new Tile(new Vector2Int(0 ,0)), 
                new Tile(new Vector2Int(0, 1)), 
                new Tile(new Vector2Int(0, 2), new (string id, Position pos)[]
                {
                    ("test-skeleton-swordsman", Position.Frontline),
                    ("test-skeleton-swordsman", Position.BacklineRight),
                    ("test-skeleton-swordsman", Position.BacklineLeft),
                }),
            });
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
            _maze.Tick();
            _charactersManager.WorldPos = _maze.WorldPosition;
            _charactersManager.Tick();
            UpdateGameState();
        }

        public void SwitchState(State state)
        {
            CurrentState = state;
            switch (CurrentState)
            {
                case State.Exploration:
                    Input.Input.SetModeState(Mode.Combat, true);
                    Input.Input.SetModeState(Mode.Walking, true);
                    break;
                case State.Combat:
                    Input.Input.SetModeState(Mode.Combat, true);
                    Input.Input.SetModeState(Mode.Walking, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateGameState()
        {
            if (_charactersManager.AliveEnemies.Count > 0 && CurrentState != State.Combat)
                SwitchState(State.Combat);
            if (_charactersManager.AliveEnemies.Count == 0 && CurrentState == State.Combat)
                SwitchState(State.Exploration);
        }
    }
}