namespace RogueDungeon.Services.FSM
{
    public interface IStateEnterHandler : IStateHandler
    {
        void OnEnter();
    }
}