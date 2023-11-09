using SpecialHedgehog.Scripts.Hero;
using SpecialHedgehog.Scripts.Mobs;
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
        [field: SerializeField] public float MobAttackCooldown { get; private set; } = 0.3f;
    }
}