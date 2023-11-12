using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Audio;
using SpecialHedgehog.UnityRefs;

namespace SpecialHedgehog.PickUps
{
    public class PickUpSoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PickedUp, PickUpSounds, TransformRef>> _filter;

        private EcsPoolInject<PickUpSounds> _pickUpSoundsPool;
        private EcsPoolInject<TransformRef> _transformRefPool;

        private EcsCustomInject<AudioService> _audioService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                ref var pickUpSounds = ref _pickUpSoundsPool.Value.Get(entity);
                
                _audioService.Value.PlaySoundAtPoint(pickUpSounds.AudioClips.GetRandom(), transformRef.Value.position);
            }
        }
    }
}