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

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if (_intendedItemGetter.Item == _currentItemGetter.Item) 
                return;

            if (_currentItemGetter.Item == null)
            {
                _currentItemSetter.Item = _intendedItemGetter.Item;
                stateChanger.To<UnsheathState>();
            }
            else
                stateChanger.To<SheathState>();
        }
    }
}