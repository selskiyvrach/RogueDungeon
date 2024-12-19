using System;
using RogueDungeon.Items.Bahaviour.Common;
using RogueDungeon.Items.Bahaviour.Unsheather;
using RogueDungeon.Items.Weapons;
using UniRx;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class WeaponBehaviourEnabler : IDisposable
    {
        private readonly WeaponBehaviour _behaviour;
        private readonly ICurrentItemGetter _itemGetter;
        private readonly IDisposable _sub;

        public WeaponBehaviourEnabler(ICurrentItemUsableGetter itemUsableGetter, WeaponBehaviour behaviour, ICurrentItemGetter itemGetter)
        {
            _behaviour = behaviour;
            _itemGetter = itemGetter;
            _sub = itemUsableGetter.IsUsable.Skip(1).Subscribe(HandleItemEnabledStateChanged);
        }
        
        private void HandleItemEnabledStateChanged(bool enabled)
        {
            if(enabled && _itemGetter.Item is IWeaponInfo)
                _behaviour.Enable();
            else
                _behaviour.Disable();
        }
        
        public void Dispose() => 
            _sub?.Dispose();
    }
}