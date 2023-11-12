using UnityEngine;

namespace SpecialHedgehog.Hero
{
    [CreateAssetMenu(fileName = "Hero Config", menuName = "Data/Hero Config")]
    public class HeroConfig : ScriptableObject
    {
        [field: Header("Stats")]
        [field: SerializeField] public float HeroSpeed { get; private set; } = 200;
        [field: SerializeField] public float HeroHealth { get; private set; } = 100;
        
        [field: Header("Audio")]
        [field: SerializeField] public AudioClip[] DeathSounds { get; private set; }
        [field: SerializeField] public AudioClip[] HitSounds { get; private set; }
        [field: SerializeField] public AudioClip[] StepSounds { get; private set; }
    }
}