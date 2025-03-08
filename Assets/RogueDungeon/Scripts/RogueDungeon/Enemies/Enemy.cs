using System;
using Common.Fsm;
using Common.Lifecycle;
using Common.Time;
using Common.Unity;
using RogueDungeon.Enemies.MoveSet;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies
{
    public class SpriteSheetAnimationPlayer
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Ticker _ticker = new();
        
        private Sprite[] _sprites;
        private float _duration;
        private float _timePassed;

        public bool IsPlaying { get; private set; }

        public SpriteSheetAnimationPlayer(SpriteRenderer spriteRenderer) => 
            _spriteRenderer = spriteRenderer;

        public void Play(Sprite[] sprites, float duration)
        {
            Assert.IsTrue(sprites.Length > 0);
            _timePassed = 0;
            _duration = duration;
            _sprites = sprites;
            _ticker.Start(Tick);
            IsPlaying = true;
        }

        private void Tick(float timeDelta)
        {
            if (_timePassed < _duration)
            {
                var spriteIndex = Mathf.FloorToInt(_timePassed / _duration * _sprites.Length);
                _spriteRenderer.sprite = _sprites[spriteIndex]; 
                _timePassed += Time.deltaTime;
            }
            else
                Stop();
        }

        private void Stop()
        {
            IsPlaying = false;
            _ticker.Stop();
        }
    }

    [Serializable]
    public class AnimationConfig
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }

    public class EnemyAnimationsConfig : ScriptableObject
    {
        [field: SerializeField] public AnimationConfig BirthAnimation { get; private set; }
        [field: SerializeField] public AnimationConfig DeathAnimation { get; private set; }
        
    }
    
    
    // birth death -> lifecycle
    // idle -> returns from any state beside death
    // stun -> taking damage
    // action (can be a series as well) -> hivemind command
    
    // star-like state machine
        // state finished -> idle
        // attack command -> attack (attack id set in context)
        // no hp -> death
        // no poise -> stun
        
    // hive-mind reads the following
        // is alive
        // is performing action
    

    public class BirthState
    {
        
        // play animation
        // enter idle
    }

    public class DeathState
    {
        // play animation
        // deregister and destroy
    }

    public class IdleState
    {
        // check for death
        // play idle animation
        // check for staged attack
        
        // check for death
        // check for stun
    }

    public class AttackState
    {
        // unstage attack
        // play its animation
        // do damage
        // go idle
        
        // check for death
        // check for stun
    }
        
    public class Enemy : IInitializable, ITickable
    {
        private StateMachine _moveSetBehaviour;
        private readonly EnemyConfig _config;
        private float _currentHealth;

        public EnemyPosition TargetablePosition { get; set; }
        public EnemyPosition OccupiedPosition { get; set; }
        
        public ITwoDWorldObject WorldObject { get; }
        public bool IsAlive => _currentHealth > 0;
        public bool IsIdle => throw new NotImplementedException();
        public EnemyAttackMove[] Attacks => throw new NotImplementedException();

        public Enemy(EnemyConfig config, GameObject gameObject)
        {  
            WorldObject = new TwoDWorldObject(gameObject);
            _config = config;
            _currentHealth = _config.Health;
        }

        // creation step!
        public void SetBehaviour(StateMachine moveSetBehaviour)
        {
            Assert.IsNull(_moveSetBehaviour);
            Assert.IsNotNull(moveSetBehaviour);
            _moveSetBehaviour = moveSetBehaviour;
        }

        public void Tick(float deltaTime) => 
            _moveSetBehaviour.Tick(deltaTime);

        public void Initialize() => 
            _moveSetBehaviour.Initialize();

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public void TakeDamage(float damage) => 
            _currentHealth -= damage;

        public void PerformMove(EnemyAttackMove move)
        {
            throw new NotImplementedException();
        }
    }
}