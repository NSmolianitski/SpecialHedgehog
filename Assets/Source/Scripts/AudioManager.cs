using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SpecialHedgehog
{
    public class AudioManager : MonoBehaviour
    {
        private enum SoundState
        {
            Enabled = 1,
            Disabled = -80
        }
    
        public enum OptionType
        {
            Master,
            Music,
            Sounds
        }
    
        [SerializeField] private AudioMixer audioMixer;

        private bool _allSoundsEnabled;
        private bool _musicEnabled;
        private bool _effectSoundsEnabled;

        private const string MasterVolume = "MasterVolume";
        private const string MusicVolume = "MusicVolume";
        private const string SoundsVolume = "SoundsVolume";

        private readonly Dictionary<OptionType, AudioOption> _options = new()
        {
            {OptionType.Master, new AudioOption() { Key = MasterVolume }},
            {OptionType.Music, new AudioOption() { Key = MusicVolume }},
            {OptionType.Sounds, new AudioOption() { Key = SoundsVolume }}
        };

        public void SetOptionState(OptionType type, bool isEnabled)
        {
            var audioOption = _options[type];
            audioOption.IsEnabled = isEnabled;

            var soundState = isEnabled ? SoundState.Enabled : SoundState.Disabled;
            audioMixer.SetFloat(audioOption.Key, (int) soundState);
        }
    }
}