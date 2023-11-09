using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Framework.Services;

namespace SpecialHedgehog.Scripts.Attack
{
    public class AttackCooldownReduceSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<AttackCooldown>> _attackCooldownFilter;

        private EcsPoolInject<AttackCooldown> _attackCooldownPool;

        private EcsCustomInject<TimeService> _timeService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _attackCooldownFilter.Value)
            {
                ref var cooldown = ref _attackCooldownPool.Value.Get(entity);
                cooldown.TimeRemaining -= _timeService.Value.DeltaTime;
                
                if (cooldown.TimeRemaining <= 0)
                    _attackCooldownPool.Value.Del(entity);
            }
        }
    }
}