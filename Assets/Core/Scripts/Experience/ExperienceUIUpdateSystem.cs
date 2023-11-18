using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SpecialHedgehog.Experience
{
    public class ExperienceUIUpdateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Experience, ExperienceBarViewRef, ExperienceGained>> _filter;

        private EcsPoolInject<Experience> _experiencePool;
        private EcsPoolInject<ExperienceBarViewRef> _experienceBarViewRefPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var experience = ref _experiencePool.Value.Get(entity);
                ref var experienceBarViewRef = ref _experienceBarViewRefPool.Value.Get(entity);
                
                experienceBarViewRef.Value.UpdateExperience(experience.Current, experience.TillNextLevel);
            }
        }
    }
}