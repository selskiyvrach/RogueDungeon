using System;
using Common.Fsm;
using RogueDungeon.Items.Handling.Common;
using RogueDungeon.Items.Weapons;
using UniRx;

namespace RogueDungeon.Items.Handling.WeaponWielder
{
    public class WeaponBehaviour : StateMachine, IComboCounter, IComboInfo, IDisposable
    {
        private readonly ICurrentHandheldItemProvider _itemProvider;
        private readonly IDisposable _sub;
        
        int IComboCounter.AttackIndex { get; set; }
        AttackDirection[] IComboInfo.AttackDirectionsInCombo => ((IWeaponInfo)_itemProvider.ItemInfo).AttackDirectionsInCombo;

        public WeaponBehaviour(IStatesFactory statesFactory, ICurrentHandheldItemProvider itemProvider, ICurrentItemEnabledState itemEnabledState) : base(statesFactory)
        {
            _itemProvider = itemProvider;
            _sub = itemEnabledState.Enabled.Skip(1).Subscribe(HandleItemEnabledStateChanged);
        }

        protected override void ToStartState() => 
            To<IdleState>();

        private void HandleItemEnabledStateChanged(bool enabled)
        {
            if(enabled && _itemProvider.ItemInfo is IWeaponInfo)
                Enable();
            else
                Disable();
        }

        public void Dispose() => 
            _sub?.Dispose();
    }
}