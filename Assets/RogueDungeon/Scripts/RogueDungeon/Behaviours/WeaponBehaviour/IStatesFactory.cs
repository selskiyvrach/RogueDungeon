namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal interface IStatesFactory
    {
        T Create<T>() where T : IState;
    }
}