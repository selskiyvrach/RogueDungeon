namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal interface IState
    {
        void Enter();
        void Exit();
        void Tick(float timeDelta);
        void CheckTransitions(IStateChanger stateChanger);
    }
}