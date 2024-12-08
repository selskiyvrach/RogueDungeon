using System;
using UniRx;
using Zenject;

namespace RogueDungeon.Weapons
{
    public abstract class AttackStateChangedHandler : IDisposable, IInitializable
    {
        private readonly IAttackMediator _attackMediator;
        private IDisposable _sub;

        protected AttackStateChangedHandler(IAttackMediator attackMediator) => 
            _attackMediator = attackMediator;

        public void Initialize() => 
            _sub = _attackMediator.AttackState.Subscribe(HandleStateChanged);

        public void Dispose() => 
            _sub?.Dispose();

        protected abstract void HandleStateChanged(AttackState state);
    }
}