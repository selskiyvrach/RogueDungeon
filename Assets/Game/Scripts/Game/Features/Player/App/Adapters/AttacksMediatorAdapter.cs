using Game.Features.Combat.Domain;
using Game.Features.Player.Domain;
using Game.Libs.Items;

namespace Game.Features.Player.App.Adapters
{
    public class AttacksMediatorAdapter : IPlayerAttacksMediator
    {
        private readonly AttacksMediator _attacksMediator;

        public AttacksMediatorAdapter(AttacksMediator attacksMediator) => 
            _attacksMediator = attacksMediator;

        public void MediatePlayerAttack(IItem weapon) => 
            _attacksMediator.MediatePlayerAttack();
    }
}