namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal interface IStateChanger
    {
        void To<T>() where T : IState;
    }
}