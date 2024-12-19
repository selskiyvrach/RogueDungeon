using Common.Fsm;
using RogueDungeon.Items.Bahaviour.Common;

namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    public class EvaluateState : IState
    {
        private readonly IIntendedItemGetter _intendedItemGetter;
        private readonly IIntendedItemSetter _intendedItemSetter;
        private readonly ICurrentItemGetter _currentItemGetter;
        private readonly ICurrentItemSetter _currentItemSetter;

        public EvaluateState(IIntendedItemGetter intendedItemGetter, ICurrentItemGetter currentItemGetter, IIntendedItemSetter intendedItemSetter, ICurrentItemSetter currentItemSetter)
        {
            _intendedItemGetter = intendedItemGetter;
            _currentItemGetter = currentItemGetter;
            _intendedItemSetter = intendedItemSetter;
            _currentItemSetter = currentItemSetter;
        }

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if (_intendedItemGetter.Item == _currentItemGetter.Item) 
                return;

            if (_currentItemGetter.Item == null)
            {
                _currentItemSetter.Item = _intendedItemGetter.Item;
                _intendedItemSetter.Item = null;
                stateChanger.To<UnsheathState>();
            }
            else
                stateChanger.To<SheathState>();
        }
    }
}