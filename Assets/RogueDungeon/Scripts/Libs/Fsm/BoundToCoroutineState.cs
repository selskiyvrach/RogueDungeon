using System.Collections;
using Libs.Utils.Unity;
using UnityEngine;

namespace Libs.Fsm
{
    public abstract class BoundToCoroutineState : IState, IEnterableState, IExitableState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _routine;

        protected bool IsFinished { get; private set; }

        protected BoundToCoroutineState(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public virtual void Enter() => 
            _routine = _coroutineRunner.Run(RoutineRunner());

        public virtual void Exit()
        {
            _coroutineRunner.Stop(_routine);
            _routine = null;
        }

        private IEnumerator RoutineRunner()
        {
            IsFinished = false;
            yield return Routine();
            IsFinished = true;
        }

        protected abstract IEnumerator Routine();
    }
}