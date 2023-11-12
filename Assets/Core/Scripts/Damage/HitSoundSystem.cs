using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Audio;
using SpecialHedgehog.UnityRefs;

namespace SpecialHedgehog.Damage
{
    public class HitSoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Damaged, HitSounds, TransformRef>> _filter;

        private EcsPoolInject<HitSounds> _hitSoundsPool;
        private EcsPoolInject<TransformRef> _transformRefPool;

        private EcsCustomInject<AudioService> _audioService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var hitSounds = ref _hitSoundsPool.Value.Get(entity);
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                
                _audioService.Value.PlaySoundAtPoint(hitSounds.AudioClips.GetRandom(), transformRef.Value.position);
            }
        }
    }
}