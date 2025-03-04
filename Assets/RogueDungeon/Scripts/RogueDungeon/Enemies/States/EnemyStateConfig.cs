using UnityEngine;

namespace RogueDungeon.Enemies.States
{
    public class EnemyStateConfig : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public AnimationClip Animation { get; private set; }
    }
}