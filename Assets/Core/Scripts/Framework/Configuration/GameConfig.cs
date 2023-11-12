using SpecialHedgehog.Hero;
using SpecialHedgehog.Mobs;
using SpecialHedgehog.PickUps;
using SpecialHedgehog.Projectiles;
using UnityEngine;

namespace SpecialHedgehog.Framework.Configuration
{
    [CreateAssetMenu(menuName = "Data/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: Header("Hero")]
        [field: SerializeField] public HeroView HeroViewPrefab { get; private set; }
        
        [field: Header("Mobs")]
        [field: SerializeField] public MobView MobViewPrefab { get; private set; }
        
        [field: Header("Projectiles")]
        [field: SerializeField] public ProjectileView ProjectilePrefab { get; private set; }
        [field: SerializeField] public float ProjectileSpeed { get; private set; } = 300;
        [field: SerializeField] public float ProjectileDamage { get; private set; } = 10;
        
        [field: Header("Projectiles")]
        [field: SerializeField] public PickableView GemPrefab { get; private set; }
        [field: SerializeField] public float GemPrice { get; private set; } = 10;
    }
}