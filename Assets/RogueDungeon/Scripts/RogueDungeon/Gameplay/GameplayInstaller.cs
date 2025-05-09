using Common.UI;
using Common.UI.Bars;
using Common.UtilsZenject;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Levels;
using RogueDungeon.Player.Model;
using RogueDungeon.UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private BattleField _battleField;
        [SerializeField] private Transform _levelRoot;
        [SerializeField] private RoomLocalPositionsConfig _roomLocalPositionsConfig;
        [SerializeField] private ScreensSorter _screensSorter;
        [SerializeField] private UiManagerConfig _config;
        [SerializeField] private BarDeltaConfig _barDeltaConfig;
        [SerializeField] private CombatFeedbackConfig _feedbackConfig;
        private Gameplay _gameplay;

        public override void InstallBindings()
        {
            // Level
            Container.InstanceSingle(_roomLocalPositionsConfig);
            Container.InstanceSingle(_levelRoot);
            Container.NewSingle<IFactory<RoomEventConfig, IRoomEvent>, RoomEventFactory>();
            Container.NewSingle<IFactory<RoomConfig, Room>, RoomFactory>();
            Container.NewSingle<IFactory<LevelConfig, Level>, LevelFactory>();
            
            // Combat
            Container.InstanceSingle(_feedbackConfig);
            Container.NewSingle<CombatFeedbackPlayer>();
            Container.NewSingleInterfaces<AttacksMediator>();
            Container.NewSingleInterfaces<CombatantsRegistry>();
            
            // Enemies
            Container.InstanceSingle(_battleField);
            Container.NewSingleInterfaces<EnemyFactory>();
            Container.NewSingleInterfaces<EnemySpawner>().WithArguments(_battleField.transform);
            
            // Items
            
            
            // Player
            Container.NewSingleInterfaces<PlayerFactory>();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().FromNew().AsSingle()
                .WithArguments(_playerConfig, _playerTransform);
            
            // UI
            // gonna need it in enemy uis as well
            Container.InstanceSingle(_barDeltaConfig);
            var uiContainer = Container.CreateSubContainer();
            uiContainer.InstanceSingle(_config);
            uiContainer.InstanceSingle(_screensSorter);
            uiContainer.NewSingle<UiFactory>();
            uiContainer.NewSingle<GameplayUiManager>();
            Container.Bind<GameplayUiManager>().FromSubContainerResolve().ByInstance(uiContainer).AsSingle();
            
            // Gameplay
            Container.InstanceSingle(_gameplayConfig);
            Container.NewSingleInterfacesAndSelf<Gameplay>();
        }

        public override void Start()
        {
            base.Start();
            _gameplay = Container.Resolve<Gameplay>();
            _gameplay.Initialize();
        }

        private void Update() => 
            _gameplay.Tick(Time.deltaTime);
    }
}