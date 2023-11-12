using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Audio;
using SpecialHedgehog.Audio.Sounds;
using SpecialHedgehog.Death;
using SpecialHedgehog.UnityRefs;

namespace SpecialHedgehog.Mobs
{
    public class DeathAudioSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DeathSounds, JustDied, TransformRef>> _filter;

        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<DeathSounds> _deathSoundsPool;

        private EcsCustomInject<AudioService> _audioService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                ref var deathSounds = ref _deathSoundsPool.Value.Get(entity);
                
                _audioService.Value.PlaySoundAtPoint(deathSounds.AudioClips.GetRandom(), transformRef.Value.position);
            }
        }
    }
}