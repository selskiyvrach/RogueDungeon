using System;
using RogueDungeon.Items.Behaviours.Common;
using RogueDungeon.Items.Data.Weapons;
using UniRx;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public class WeaponWielderBehaviourEnabler : IDisposable
    {
        private readonly WeaponWielderBehaviour _wielderBehaviour;
        private readonly ICurrentItemGetter _itemGetter;
        private readonly IDisposable _sub;

        public WeaponWielderBehaviourEnabler(ICurrentItemUsableGetter itemUsableGetter, WeaponWielderBehaviour wielderBehaviour, ICurrentItemGetter itemGetter)
        {
            _wielderBehaviour = wielderBehaviour;
            _itemGetter = itemGetter;
            _sub = itemUsableGetter.IsUsable.Skip(1).Subscribe(HandleItemEnabledStateChanged);
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