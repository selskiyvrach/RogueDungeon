using Common.Animations;
using Common.UtilsZenject;
using RogueDungeon.Characters.Commands;
using RogueDungeon.Items.Data.Weapons;
using RogueDungeon.Player.Behaviours.Items.WeaponWielder;
using RogueDungeon.Player.Input;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private DodgeAnimationsPlayer _animationPlayer;
        [SerializeField] private WeaponConfig _weaponCofig;
        private Player _player;

        public override void InstallBindings()
        {
            Container.NewSingleInterfaces<PlayerControlStateMediator>();
            Container.AutoResolve<PlayerControlStateMediator>();
            
            Container.NewSingle<ICharacterCommands, PlayerInput>();
            Container.InstanceSingle(_weaponCofig);
            Container.InstanceSingle<IAnimator>(_animationPlayer);
            Container.NewSingleInterfacesAndSelf<Player>();
            Container.AutoResolve<Player>();

            Container.NewSingle<AttackHitEventHandler>();
            Container.AutoResolve<AttackHitEventHandler>();
        }
    }

    public class AttackHitEventHandler
    {
        private readonly IAttackHitEventObservable _attackHitEvent;

        public AttackHitEventHandler(IAttackHitEventObservable attackHitEvent)
        {
            _attackHitEvent = attackHitEvent;
            _attackHitEvent.OnHit += HandleHit;
        }

        private void HandleHit() => 
            Debug.LogError("Behold attack hit event, everybody");
    }
}