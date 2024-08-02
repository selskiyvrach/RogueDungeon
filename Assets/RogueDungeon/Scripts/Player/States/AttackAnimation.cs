using System;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.States
{
    public class AttackAnimation : AnimationPlayer, IAttackAnimation
    {
        public event Action OnHitKeyframe;

        protected override void OnEvent(int eventIndex)
        {
            Assert.AreEqual(eventIndex, 0);
            
        }
    }
}