using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SpecialHedgehog.Experience
{
    public class ExperienceGainSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Experience, Level, ExperienceGained>> _filter;

        private EcsPoolInject<Experience> _experiencePool;
        private EcsPoolInject<Level> _levelPool;
        private EcsPoolInject<LevelUp> _levelUpPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var experience = ref _experiencePool.Value.Get(entity);

                experience.Current += experience.JustGained;
                experience.JustGained = 0;
                
                if (experience.Current >= experience.TillNextLevel)
                {
                    experience.Current -= experience.TillNextLevel;
                    _levelUpPool.Value.Add(entity);
                }
            }
        }   
    }
}