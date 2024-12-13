namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal abstract class State : IState
    {
        public  abstract void Enter();
        public virtual void Exit()
        {
        }

        public virtual void Tick(float timeDelta)
        {
        }

        public abstract void CheckTransitions(IStateChanger stateChanger);
    }
}