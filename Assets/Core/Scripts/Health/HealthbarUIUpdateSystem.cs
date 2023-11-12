using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Damage;

namespace SpecialHedgehog.Health
{
    public class HealthbarUIUpdateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Health, HealthbarViewRef, Damaged>> _filter;

        private EcsPoolInject<Health> _healthPool;
        private EcsPoolInject<HealthbarViewRef> _healthbarViewRefPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var health = ref _healthPool.Value.Get(entity);
                ref var healthbarRef = ref _healthbarViewRefPool.Value.Get(entity);
                
                healthbarRef.Value.UpdateHealth(health.Current, health.Max);
            }
        }
    }
}