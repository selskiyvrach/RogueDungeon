using System;
using System.Collections;
using Common.Unity;
using Common.UtilsDotNet;
using RogueDungeon.Combat;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies.Attacks
{
    public interface IEnemyAction
    {
        EnemyActionType Type { get; }
        void Start();
        void Stop();
        bool IsFinished { get; }
    }

    [Serializable]
    public abstract class EnemyActionConfig
    {
        [field: SerializeField] public EnemyActionType Type { get; private set; }
        [field: SerializeField] public float PrepareDuration { get; private set; }
        [field: SerializeField] public Sprite[] PrepareSprites { get; private set; }
        [field: SerializeField] public float ExecuteDuration { get; private set; }
        [field: SerializeField] public Sprite[] ExecuteSprites { get; private set; }
        public abstract Type ActionType { get; }
    }

    [Serializable]
    public class EnemyAttackActionConfig : EnemyActionConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public EnemyAttackDirection AttackDirection { get; private set; }
        public override Type ActionType => typeof(EnemyAttackAction);
    }

    public class EnemyAttackAction : EnemyAction
    {
        private readonly IAttacksMediator _attacksMediator;
        private readonly EnemyAttackActionConfig _config;

        public EnemyAttackAction(EnemyAttackActionConfig config, SpriteRenderer spriteRenderer,
            IAttacksMediator attacksMediator, ICoroutineRunner coroutineRunner) : base(config, spriteRenderer, coroutineRunner)
        {
            _config = config;
            _attacksMediator = attacksMediator;
        }

        public bool IsSuitableForPosition(EnemyPosition position) =>
            position == EnemyPosition.Middle || (position == EnemyPosition.Left
                ? _config.AttackDirection == EnemyAttackDirection.Left
                : position == EnemyPosition.Right && _config.AttackDirection == EnemyAttackDirection.Right);

        protected override void ExecuteEffect() => 
            _attacksMediator.MediateEnemyAttack(_config.Damage, _config.AttackDirection.ThrowIfNone());
    }

    public abstract class EnemyAction : IEnemyAction
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly EnemyActionConfig _config;
        private float _timePassed;
        private Coroutine _coroutine;

        public bool IsFinished => _coroutine == null;
        public EnemyActionType Type => _config.Type;

        protected EnemyAction(EnemyActionConfig config, SpriteRenderer spriteRenderer, ICoroutineRunner coroutineRunner)
        {
            _config = config;
            _spriteRenderer = spriteRenderer;
            _coroutineRunner = coroutineRunner;
        }

        public void Start()
        {
            Assert.IsTrue(IsFinished);
            _timePassed = 0;
            _coroutine = _coroutineRunner.Run(Perform());
        }

        public void Stop()
        {
            Assert.IsFalse(IsFinished);
            _coroutineRunner.Stop(_coroutine);
            _coroutine = null;
        }

        public IEnumerator Perform()
        {
            while (_timePassed < _config.PrepareDuration)
            {
                var spriteIndex = Mathf.FloorToInt(_timePassed / _config.PrepareDuration * _config.PrepareSprites.Length);
                _spriteRenderer.sprite = _config.PrepareSprites[spriteIndex]; 
                _timePassed += Time.deltaTime;
                yield return null;
            }

            ExecuteEffect();
            _timePassed = 0;
            while (_timePassed < _config.ExecuteDuration)
            {
                var spriteIndex = Mathf.RoundToInt(_timePassed / _config.ExecuteDuration * _config.ExecuteSprites.Length);
                _spriteRenderer.sprite = _config.ExecuteSprites[spriteIndex]; 
                _timePassed += Time.deltaTime;
                yield return null;
            }
            Stop();
        }

        protected abstract void ExecuteEffect();
    }

    public enum EnemyActionType
    {
        None,
        LightAttack,
        HeavyAttack,
    }
}