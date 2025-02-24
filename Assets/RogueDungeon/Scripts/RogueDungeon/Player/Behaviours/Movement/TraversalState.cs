using Common.Fsm;
using Common.Unity;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public abstract class TraversalState : ITypeBasedTransitionableState, IEnterableState, ITickableState
    {
        private float _timePassed;
        
        protected readonly LevelTraverserConfig Config;
        protected abstract float Duration { get; }
        protected readonly ILevelTraverser LevelTraverser;
        public bool IsFinished => _timePassed >= Duration;

        protected TraversalState(ILevelTraverser levelTraverser, LevelTraverserConfig config)
        {
            LevelTraverser = levelTraverser;
            Config = config;
        }

        public virtual void Enter() => 
            _timePassed = 0;

        public void Tick(float deltaTime)
        {
            if(!IsFinished)
                SetValueNormalized((_timePassed += deltaTime) / Duration);
        }

        protected abstract void SetValueNormalized(float value);

        protected Vector2 GetPositionInTileWithOffset(Vector2Int targetTile) => 
            targetTile + LevelTraverser.Direction * -Config.PositionOffsetFromTileCenter;


        public void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(IsFinished)
                stateChanger.ChangeState<TraversalIdleState>();
        }
    }
}