using System.Collections.Generic;
using System.Linq;
using Common.Animations;
using Common.UtilsDotNet;
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
            var animation = config.AdditionalAnimations.Length == 0 
                ? CreateAnimation(config.AnimationConfigPicker.Config) 
                : _container.Instantiate(typeof(CompositeAnimation), new []{ config.AnimationConfigPicker.Config.AsEnumerable().Concat(config.AdditionalAnimations.Select(n => n.Config)).Select(CreateAnimation)});
            return (EnemyState)_container.Instantiate(config.StateType, new[] { config,animation } );
        }

        private IAnimation CreateAnimation(AnimationConfig config) => 
            (IAnimation)_container.Instantiate(config.AnimationType, new object[]{config});
    }
}