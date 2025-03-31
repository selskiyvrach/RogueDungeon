using System;
using Common.UI;
using RogueDungeon.Characters;
using RogueDungeon.Enemies;
using RogueDungeon.Enemies.States;
using RogueDungeon.UI;
using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class EnemyStunDurationBarViewModel : BarViewModel, IReadOnlyResource, IHideableUiElement 
    {
        private readonly Enemy _enemy;
        public float Current => (_enemy.CurrentState as EnemyStunState)?.Current ?? 0;
        public float Max => (_enemy.CurrentState as EnemyStunState)?.Max ?? float.MaxValue;
        public IReadOnlyReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();
        public event Action OnChanged;

        public EnemyStunDurationBarViewModel(Enemy enemy)
        {
            _enemy = enemy;
            _enemy.OnStateChanged += HandleStateChange;
            SetResource(this);
        }

        private void HandleStateChange(EnemyState from, EnemyState to)
        {
            if (from is EnemyStunState stunState1) 
                stunState1.OnChanged -= PropagateOnChanged;
            
            if (to is EnemyStunState stunState2) 
                stunState2.OnChanged += PropagateOnChanged;
            
            ((ReactiveProperty<bool>)IsVisible).Value = _enemy.CurrentState is EnemyStunState;
            
            PropagateOnChanged();
        }
        
        private void PropagateOnChanged() => 
            OnChanged?.Invoke();
    }
}