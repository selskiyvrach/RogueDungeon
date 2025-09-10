using Game.Features.Combat.Domain.Enemies;
using UnityEngine;

namespace Game.Features.Combat.App.Presenters
{
    public class EnemyStunBarPresenter
    {
        public EnemyStunBarPresenter(Enemy enemy, GameObject stunBar)
        {
            // enemy.OnStunnedStatusChanged += () => stunBar.SetActive(enemy.IsStunned);
            // stunBar.SetActive(enemy.IsStunned);
        }
    }
}