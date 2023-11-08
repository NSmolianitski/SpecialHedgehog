using SpecialHedgehog.Scripts.Enemies;
using SpecialHedgehog.Scripts.Hero;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Configuration
{
    [CreateAssetMenu(menuName = "Data/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: Header("Hero")]
        [field: SerializeField] public HeroView HeroViewPrefab { get; private set; }
        [field: SerializeField] public float HeroSpeed { get; private set; } = 200;
        
        [field: Header("Enemies")]
        [field: SerializeField] public EnemyView EnemyViewPrefab { get; private set; }
        [field: SerializeField] public float EnemySpeed { get; private set; } = 150;
    }
}