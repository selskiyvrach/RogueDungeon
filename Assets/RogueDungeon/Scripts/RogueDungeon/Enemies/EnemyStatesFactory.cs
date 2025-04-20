using RogueDungeon.Enemies.States;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyStatesFactory : IFactory<EnemyStateConfig, EnemyState>
    {
        private readonly DiContainer _container;

        public EnemyStatesFactory(DiContainer container) => 
            _container = container;

        public EnemyState Create(EnemyStateConfig config) => 
            (EnemyState)_container.Instantiate(config.StateType, new object[] { config, config.AnimationConfigPicker.Config.Create(_container) } );
    }
}