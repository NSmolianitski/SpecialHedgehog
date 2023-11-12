using UnityEngine;

namespace SpecialHedgehog.Mobs
{
    [CreateAssetMenu(fileName = "Mob Config", menuName = "Data/MobConfig")]
    public class MobConfig : ScriptableObject
    {
        [field: Header("Stats")]
        [field: SerializeField] public float Speed { get; private set; } = 150;
        [field: SerializeField] public float Damage { get; private set; } = 5;
        [field: SerializeField] public float Health { get; private set; } = 20;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 0.3f;
        
        [field: Header("Audio")]
        [field: SerializeField] public AudioClip[] DeathSounds { get; private set; }
        [field: SerializeField] public AudioClip[] HitSounds { get; private set; }
        [field: SerializeField] public AudioClip[] StepSounds { get; private set; }
    }
}