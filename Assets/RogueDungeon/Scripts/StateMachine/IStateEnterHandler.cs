namespace RogueDungeon.StateMachine
{
    public interface IStateEnterHandler : IStateHandler
    {
        void OnEnter();
    }
}