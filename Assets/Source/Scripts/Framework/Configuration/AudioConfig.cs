using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Configuration
{
    [CreateAssetMenu(fileName = "Audio Configuration", menuName = "Data/Audio Configuration", order = 1)]
    public class AudioConfig : ScriptableObject
    {
        [field: SerializeField] public float DefaultMasterVolume { get; private set; } = 1;
        [field: SerializeField] public float DefaultMusicVolume { get; private set; } = 1;
        [field: SerializeField] public float DefaultSoundVolume { get; private set; } = 1;
        
        [field: Header("Menu Sounds")]
        [field: SerializeField] public AudioClip[] MenuButtonHoverSounds { get; private set; }
        [field: SerializeField] public AudioClip[] MenuButtonClickSounds { get; private set; }
    }
}