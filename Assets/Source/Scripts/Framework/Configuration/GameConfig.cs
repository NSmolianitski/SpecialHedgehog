using SpecialHedgehog.Scripts.Hero;
using SpecialHedgehog.Scripts.Mobs;
using SpecialHedgehog.Scripts.Projectiles;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Configuration
{
    [CreateAssetMenu(menuName = "Data/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: Header("Hero")]
        [field: SerializeField] public HeroView HeroViewPrefab { get; private set; }
        [field: SerializeField] public float HeroSpeed { get; private set; } = 200;
        [field: SerializeField] public float HeroHealth { get; private set; } = 100;
        
        [field: Header("Mobs")]
        [field: SerializeField] public MobView MobViewPrefab { get; private set; }
        [field: SerializeField] public float MobSpeed { get; private set; } = 150;
        [field: SerializeField] public float MobDamage { get; private set; } = 5;
        [field: SerializeField] public float MobHealth { get; private set; } = 20;
        [field: SerializeField] public float MobAttackCooldown { get; private set; } = 0.3f;
        
        [field: Header("Projectiles")]
        [field: SerializeField] public ProjectileView ProjectilePrefab { get; private set; }
        [field: SerializeField] public float ProjectileSpeed { get; private set; } = 300;
        [field: SerializeField] public float ProjectileDamage { get; private set; } = 10;
    }
}