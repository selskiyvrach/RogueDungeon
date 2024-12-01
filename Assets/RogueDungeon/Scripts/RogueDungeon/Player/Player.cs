using System;
using Common.Events;
using Common.InstallerGenerator;
using Common.Properties;
using RogueDungeon.Animations;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;

namespace RogueDungeon.Player
{
    [CreateFactoryInstaller]
    public class Player : IGameEntity, IDodger
    {
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private IDisposable _sub;

        public Positions Position => _dodgeState.Value.ToPlayerPosition();
        public DodgeState DodgeState => _dodgeState.Value;

        private readonly Property<AttackState> _attackState;
        private readonly Property<DodgeState> _dodgeState;

        public Player(Property<AttackState> attackState, Property<DodgeState> dodgeState)
        {
            _attackState = attackState;
            _dodgeState = dodgeState;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}