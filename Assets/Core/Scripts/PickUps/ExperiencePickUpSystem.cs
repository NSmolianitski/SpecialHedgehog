using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Experience;
using SpecialHedgehog.Extensions;

namespace SpecialHedgehog.PickUps
{
    public class ExperiencePickUpSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Gem, PickedUp, Price>> _filter;

        private EcsPoolInject<PickedUp> _pickedUpPool;
        private EcsPoolInject<ExperienceGained> _gainedExperiencePool;
        private EcsPoolInject<Experience.Experience> _experiencePool;
        private EcsPoolInject<Price> _pricePool;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var pickedUp = ref _pickedUpPool.Value.Get(entity);
                pickedUp.PickerPackedEntity.Unpack(_world.Value, out var pickerEntity);

                ref var price = ref _pricePool.Value.Get(entity);
                ref var experience = ref _experiencePool.Value.Get(pickerEntity);
                experience.JustGained += price.Value;

                _gainedExperiencePool.Value.TryAdd(pickerEntity);
            }
        }
    }
}