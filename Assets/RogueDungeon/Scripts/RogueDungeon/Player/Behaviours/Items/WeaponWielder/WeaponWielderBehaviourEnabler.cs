using System;
using RogueDungeon.Items.Data.Weapons;
using RogueDungeon.Player.Behaviours.Items.Unsheather;
using UniRx;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponWielderBehaviourEnabler : IDisposable
    {
        private readonly ICurrentItemUsableGetter _itemUsableGetter;
        private readonly WeaponWielderBehaviour _wielderBehaviour;
        private readonly ICurrentItemGetter _itemGetter;
        private readonly IDisposable _sub;

        public WeaponWielderBehaviourEnabler(ICurrentItemUsableGetter itemUsableGetter, WeaponWielderBehaviour wielderBehaviour, ICurrentItemGetter itemGetter)
        {
            _itemUsableGetter = itemUsableGetter;
            _wielderBehaviour = wielderBehaviour;
            _itemGetter = itemGetter;
            _sub = Observable.EveryUpdate().Subscribe(_ => CheckItemState());
        }

        private void CheckItemState()
        {
            if(_wielderBehaviour.IsEnabled != _itemUsableGetter.IsUsable)
                HandleItemEnabledStateChanged(_itemUsableGetter.IsUsable);
        }

        private void HandleItemEnabledStateChanged(bool enabled)
        {
            if(enabled && _itemGetter.Item is IWeaponInfo)
                _wielderBehaviour.Enable();
            else
                _wielderBehaviour.Disable();
        }
        
        public void Dispose() => 
            _sub?.Dispose();
    }
}