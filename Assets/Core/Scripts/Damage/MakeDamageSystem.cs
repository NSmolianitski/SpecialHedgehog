using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Death;
using SpecialHedgehog.Extensions;
using SpecialHedgehog.Framework;

namespace SpecialHedgehog.Damage
{
    public class MakeDamageSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MakeDamageRequest>> _makeDamageFilter = Constants.Worlds.Events;

        private EcsPoolInject<MakeDamageRequest> _makeDamageRequestPool = Constants.Worlds.Events;
        
        private EcsPoolInject<Health.Health> _healthPool;
        private EcsPoolInject<Dead> _deadPool;
        private EcsPoolInject<Damaged> _damagedPool;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _makeDamageFilter.Value)
            {
                ref var makeDamageRequest = ref _makeDamageRequestPool.Value.Get(entity);

                makeDamageRequest.Target.Unpack(_world.Value, out var damageTargetEntity);
                
                ref var health = ref _healthPool.Value.Get(damageTargetEntity);
                health.Current -= makeDamageRequest.Value;
                
                if (health.Current <= 0)
                {
                    health.Current = 0;
                    _deadPool.Value.TryAdd(damageTargetEntity);
                }

                _damagedPool.Value.TryAdd(damageTargetEntity);
            }
        }
    }
}