using UnityEngine;

namespace Game.Libs.Items.Configs
{
    public class MapViewConfig : ScriptableObject
    {
        [field: SerializeField] public float TileSpacing { get; private set; } = .002f;
        [field: SerializeField] public Sprite Corridor { get; private set; }
        [field: SerializeField] public Sprite XJoint { get; private set; }
        [field: SerializeField] public Sprite TJoint { get; private set; }
        [field: SerializeField] public Sprite LJoint { get; private set; }
        [field: SerializeField] public SpriteRenderer PlayerMarkerPrefab { get; private set; }
        [field: SerializeField] public SpriteRenderer TilePrefab { get; private set; }
    }
}