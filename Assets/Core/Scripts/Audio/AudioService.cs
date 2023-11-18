using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SpecialHedgehog.Audio
{
    public class AudioService : MonoBehaviour
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
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource soundsAudioSource;

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

        public void PlaySound(AudioClip clip)
        {
            soundsAudioSource.PlayOneShot(clip);
        }
        
        public void PlaySoundAtPoint(AudioClip clip, Vector3 point)
        {
            soundsAudioSource.transform.position = point;
            soundsAudioSource.PlayOneShot(clip);
        }
        
        public void PlayInterruptibleSoundAtPoint(AudioClip clip, Vector3 point)
        {
            soundsAudioSource.transform.position = point;
            soundsAudioSource.clip = clip;
            soundsAudioSource.Play();
        }

        public void PlayMusic(AudioClip clip)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.time = 1;
            musicAudioSource.Play();
        }
    }
}