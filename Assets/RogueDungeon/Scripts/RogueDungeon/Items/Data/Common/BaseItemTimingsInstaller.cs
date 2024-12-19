using Common.UtilsZenject;
using RogueDungeon.Items.Bahaviour.Unsheather;
using RogueDungeon.Items.Bahaviour.WeaponWielder;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items
{
    public class BaseItemTimingsInstaller : MonoInstaller
    {
        [SerializeField] private BaseItemTimings _timings;
        
        public override void InstallBindings()
        {
            Container.InstanceSingle<IIdleAnimationSpeed>(new IdleAnimationSpeed(() => _timings.IdleAnimationSpeed));
            Container.InstanceSingle<IAttackExecutionDuration>(new AttackExecutionDuration(() => _timings.AttackExecutionDuration));
            Container.InstanceSingle<IAttackAttackTransitionDuration>(new AttackAttackTransitionDuration(() => _timings.AttackAttackTransitionDuration));
            Container.InstanceSingle<IAttackIdleTransitionDuration>(new AttackIdleTransitionDuration(() => _timings.AttackIdleTransitionDuration));
            Container.InstanceSingle<IUnsheathDuration>(new UnsheathDuration(() => _timings.UnsheathDuration));
        }
    }
}