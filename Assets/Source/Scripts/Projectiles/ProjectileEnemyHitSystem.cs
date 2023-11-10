using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Damage;
using SpecialHedgehog.Scripts.Death;
using SpecialHedgehog.Scripts.Framework;

namespace SpecialHedgehog.Scripts.Projectiles
{
    public class ProjectileEnemyHitSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Projectile, FirstEnemyHit, DamageStat>> _filter;

        private EcsPoolInject<FirstEnemyHit> _firstEnemyHitPool;
        private EcsPoolInject<MakeDamageRequest> _makeDamageRequestPool = Constants.Worlds.Events;
        private EcsPoolInject<DamageStat> _damageStatPool;
        private EcsPoolInject<DieAfterTime> _dieAfterTimePool;
        private EcsPoolInject<Dead> _deadPool;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var firstEnemyHitPool = ref _firstEnemyHitPool.Value.Get(entity);
                
                ref var makeDamageRequest = ref _makeDamageRequestPool.NewEntity(out _);
                makeDamageRequest.Dealer = _world.Value.PackEntity(entity);
                makeDamageRequest.Target = firstEnemyHitPool.HitEnemyPackedEntity;

                ref var damageStat = ref _damageStatPool.Value.Get(entity);
                makeDamageRequest.Value = damageStat.CurrentValue;
                
                _dieAfterTimePool.Value.Del(entity);
                _deadPool.Value.Add(entity);
            }
        }
    }
}