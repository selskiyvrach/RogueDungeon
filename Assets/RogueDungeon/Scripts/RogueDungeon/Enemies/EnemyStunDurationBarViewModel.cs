using System;
using Characters;
using Common.UI;
using Enemies.States;
using UI;
using UniRx;

namespace Enemies
{
    public class EnemyStunDurationBarViewModel : BarViewModel, IReadOnlyResource, IHideableUiElement 
    {
        private readonly Enemy _enemy;
        public float Current => _enemy.CurrentMove is EnemyStaggerMove ? Max : 0;
        public float Max => 1;
        public IReadOnlyReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();
        public event Action OnChanged;

        public EnemyStunDurationBarViewModel(Enemy enemy)
        {
            _enemy = enemy;
            _enemy.OnStateChanged += HandleStateChange;
            SetResource(this);
        }

        private void HandleStateChange(EnemyMove from, EnemyMove to)
        {
            if (from is EnemyStaggerMove stunState1) 
                stunState1.OnChanged -= PropagateOnChanged;
            
            if (to is EnemyStaggerMove stunState2) 
                stunState2.OnChanged += PropagateOnChanged;
            
            ((ReactiveProperty<bool>)IsVisible).Value = _enemy.CurrentMove is EnemyStaggerMove;
            
            PropagateOnChanged();
        }
        
        private void PropagateOnChanged() => 
            OnChanged?.Invoke();
    }
}