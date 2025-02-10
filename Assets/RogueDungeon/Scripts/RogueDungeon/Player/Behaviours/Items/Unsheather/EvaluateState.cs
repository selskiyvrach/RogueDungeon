using Common.Fsm;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class EvaluateState : IState
    {
        private readonly IIntendedCurrentItemGetter _intendedItemGetter;
        private readonly ICurrentItemGetter _currentItemGetter;
        private readonly ICurrentItemSetter _currentItemSetter;

        public EvaluateState(IIntendedCurrentItemGetter intendedItemGetter, ICurrentItemGetter currentItemGetter, ICurrentItemSetter currentItemSetter)
        {
            _intendedItemGetter = intendedItemGetter;
            _currentItemGetter = currentItemGetter;
            _currentItemSetter = currentItemSetter;
        }

        public void CheckTransitions(ITypeBasedStateChanger typeBasedStateChanger)
        {
            if (_intendedItemGetter.Item == _currentItemGetter.Item) 
                return;

            if (_currentItemGetter.Item == null)
            {
                _currentItemSetter.Item = _intendedItemGetter.Item;
                typeBasedStateChanger.ChangeState<UnsheathState>();
            }
            else
                typeBasedStateChanger.ChangeState<SheathState>();
        }
    }
}