using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework.Services;

namespace SpecialHedgehog.Attack
{
    public class AttackCooldownReduceSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<AttackCooldown, AttackOnCooldown>> _attackCooldownFilter;

        private EcsPoolInject<AttackCooldown> _attackCooldownPool;
        private EcsPoolInject<AttackOnCooldown> _attackOnCooldownPool;

        private EcsCustomInject<TimeService> _timeService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _attackCooldownFilter.Value)
            {
                ref var cooldown = ref _attackCooldownPool.Value.Get(entity);
                cooldown.TimeRemaining -= _timeService.Value.DeltaTime;
                
                if (cooldown.TimeRemaining <= 0)
                    _attackOnCooldownPool.Value.Del(entity);
            }
        }
    }
}