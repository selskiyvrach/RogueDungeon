using RogueDungeon.Enemies.States;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyStatesFactory : IFactory<EnemyStateConfig, EnemyState>
    {
        private readonly DiContainer _container;

        public EnemyStatesFactory(DiContainer container) => 
            _container = container;

        public EnemyState Create(EnemyStateConfig config)
        {
            var animation = _container.Instantiate(config.AnimationConfigPicker.Config.AnimationType, new []{config.AnimationConfigPicker.Config});
            return (EnemyState)_container.Instantiate(config.StateType, new[] { config,animation } );
        }
    }
}