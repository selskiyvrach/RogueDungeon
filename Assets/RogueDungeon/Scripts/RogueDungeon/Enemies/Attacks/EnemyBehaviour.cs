using System;
using System.Collections;
using Common.Unity;
using Common.UtilsDotNet;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies.Attacks
{
    [Serializable]
    public class EnemyAttackConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public EnemyAttackDirection AttackDirection { get; private set; }
        [field: SerializeField] public float PrepareDuration { get; private set; }
        [field: SerializeField] public Sprite[] PrepareSprites { get; private set; }
        [field: SerializeField] public float ExecuteDuration { get; private set; }
        [field: SerializeField] public Sprite[] ExecuteSprites { get; private set; }
    }

    public class EnemyAttackAction
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IEnemyAttacksMediator _attacksMediator;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly EnemyAttackConfig _config;
        private float _timePassed;
        private Coroutine _coroutine;

        public bool IsFinished => _coroutine == null;

        protected EnemyAttackAction(SpriteRenderer spriteRenderer, ICoroutineRunner coroutineRunner, EnemyAttackConfig config, IEnemyAttacksMediator attacksMediator)
        {
            _spriteRenderer = spriteRenderer;
            _coroutineRunner = coroutineRunner;
            _config = config;
            _attacksMediator = attacksMediator;
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
        
        public bool IsSuitableForPosition(EnemyPosition position) =>
            position == EnemyPosition.Middle || (position == EnemyPosition.Left
                ? _config.AttackDirection == EnemyAttackDirection.Left
                : position == EnemyPosition.Right && _config.AttackDirection == EnemyAttackDirection.Right);

        private IEnumerator Perform()
        {
            while (_timePassed < _config.PrepareDuration)
            {
                var spriteIndex = Mathf.FloorToInt(_timePassed / _config.PrepareDuration * _config.PrepareSprites.Length);
                _spriteRenderer.sprite = _config.PrepareSprites[spriteIndex]; 
                _timePassed += Time.deltaTime;
                yield return null;
            }
            
            _attacksMediator.MediateEnemyAttack(_config.Damage, _config.AttackDirection.ThrowIfNone());
            
            _timePassed = 0;
            while (_timePassed < _config.ExecuteDuration)
            {
                var spriteIndex = Mathf.FloorToInt(_timePassed / _config.ExecuteDuration * _config.ExecuteSprites.Length);
                _spriteRenderer.sprite = _config.ExecuteSprites[spriteIndex]; 
                _timePassed += Time.deltaTime;
                yield return null;
            }
            Stop();
        }
    }
}